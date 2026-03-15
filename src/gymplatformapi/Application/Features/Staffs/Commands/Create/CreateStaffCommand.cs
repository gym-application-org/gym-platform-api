using Application.Constants;
using Application.Features.Staffs.Constants;
using Application.Features.Staffs.Rules;
using Application.Features.Tenants.Commands.Create;
using Application.Features.Tenants.Constants;
using Application.Services.MailServices.UserOnboardingMails;
using Application.Services.OperationClaims;
using Application.Services.Repositories;
using Application.Services.Staffs;
using Application.Services.Tenants;
using Application.Services.UserActionTokens;
using Application.Services.UserOperationClaims;
using Application.Services.UsersService;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.UserActionToken;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Commands.Create;

public class CreateStaffCommand : IRequest<CreatedStaffResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest, ITenantRequest
{
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner];

    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, CreatedStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStaffRepository _staffRepository;
        private readonly StaffBusinessRules _staffBusinessRules;
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IUserActionTokenService _userActionTokenService;
        private readonly IUserOnboardingMailService _userOnboardingMailService;
        private readonly ICurrentUser _currentUser;
        private readonly IUserActionTokenHelper _userActionTokenHelper;
        private readonly ICurrentTenant _currentTenant;
        private readonly ITenantService _tenantService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;

        public CreateStaffCommandHandler(
            IMapper mapper,
            IStaffRepository staffRepository,
            StaffBusinessRules staffBusinessRules,
            ICurrentTenant currentTenant,
            IUserService userService,
            ICurrentUser currentUser,
            IUserActionTokenService userActionTokenService,
            IUserOnboardingMailService userOnboardingMailService,
            IUserActionTokenHelper userActionTokenHelper,
            IStaffService staffService,
            ITenantService tenantService,
            IUserOperationClaimService userOperationClaimService,
            IOperationClaimService operationClaimService
        )
        {
            _mapper = mapper;
            _staffRepository = staffRepository;
            _staffService = staffService;
            _staffBusinessRules = staffBusinessRules;
            _currentTenant = currentTenant;
            _userService = userService;
            _userActionTokenHelper = userActionTokenHelper;
            _userActionTokenService = userActionTokenService;
            _userOnboardingMailService = userOnboardingMailService;
            _currentUser = currentUser;
            _tenantService = tenantService;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
        }

        public async Task<CreatedStaffResponse> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            Guid tenantId = _currentTenant.TenantId!.Value;
            int userId = _currentUser.UserId;

            Tenant? tenant = await _tenantService.GetAsync(x => x.Id == tenantId, cancellationToken: cancellationToken);
            await _staffBusinessRules.TenantShouldExistWhenSelected(tenant);

            User user = _mapper.Map<User>(request);
            user.Status = false;
            user.MustChangePassword = true;
            user = await _userService.AddAsync(user);

            OperationClaim? claim = await _operationClaimService.GetAsync(
                x => x.Name.Equals(GeneralOperationClaims.Staff),
                cancellationToken: cancellationToken
            );
            await _staffBusinessRules.OperationClaimShouldExistWhenSelected(claim);

            UserOperationClaim userClaim = new UserOperationClaim(userId, claim!.Id);
            await _userOperationClaimService.AddAsync(userClaim);

            Staff staff = _mapper.Map<Staff>(request);
            staff.Role = Domain.Enums.StaffRole.Staff;
            staff.IsActive = false;
            staff.UserId = user.Id;
            staff.TenantId = tenantId;
            staff = await _staffService.AddAsync(staff);

            string token = _userActionTokenHelper.CreateActionToken();
            HashingHelper.CreateActionTokenHash(token, out byte[] tokenHash);

            DateTime utcNow = DateTime.UtcNow;
            UserActionToken userActionToken = new UserActionToken()
            {
                CreatedByUserId = userId,
                UserId = user.Id,
                CreatedDate = utcNow,
                ExpiresAt = utcNow.Add(ExpirationTimes.ActivationLinkExpiration),
                Email = user.Email,
                Purpose = Domain.Enums.UserActionPurpose.AccountActivation,
                TargetEntityId = staff.Id,
                TargetType = Domain.Enums.UserActionTargetType.Staff,
                TenantId = tenantId,
                TokenHash = tokenHash
            };
            await _userActionTokenService.AddAsync(userActionToken);

            string activationLink = $"{LinkEndpoints.ActivationLinkEndpoint}?token={token}&email={user.Email}";
            await _userOnboardingMailService.SendStaffInviteAsync(
                user.Email,
                staff.Name,
                tenant!.Name,
                tenant.Subdomain,
                activationLink,
                cancellationToken
            );

            CreatedStaffResponse response = _mapper.Map<CreatedStaffResponse>(staff);
            return response;
        }
    }
}
