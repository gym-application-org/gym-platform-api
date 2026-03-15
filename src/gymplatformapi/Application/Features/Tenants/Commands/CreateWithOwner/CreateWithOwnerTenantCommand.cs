using Application.Constants;
using Application.Features.Staffs.Rules;
using Application.Features.Tenants.Constants;
using Application.Features.Tenants.Rules;
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
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.UserActionToken;
using Domain.Entities;
using MediatR;
using static Application.Features.Tenants.Constants.TenantsOperationClaims;

namespace Application.Features.Tenants.Commands.Create;

public class CreateWithOwnerTenantCommand
    : IRequest<CreatedTenantWithOwnerResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string TenantName { get; set; }
    public bool TenantIsActive { get; set; }
    public string TenantSubdomain { get; set; }
    public string OwnerName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTenants"];

    public class CreateTenantCommandHandler : IRequestHandler<CreateWithOwnerTenantCommand, CreatedTenantWithOwnerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITenantRepository _tenantRepository;
        private readonly TenantBusinessRules _tenantBusinessRules;
        private readonly IUserService _userService;
        private readonly ITenantService _tenantService;
        private readonly IStaffService _staffService;
        private readonly IUserActionTokenService _userActionTokenService;
        private readonly IUserOnboardingMailService _userOnboardingMailService;
        private readonly ICurrentUser _currentUser;
        private readonly IUserActionTokenHelper _userActionTokenHelper;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public CreateTenantCommandHandler(
            IMapper mapper,
            ITenantRepository tenantRepository,
            TenantBusinessRules tenantBusinessRules,
            IUserService userService,
            ITenantService tenantService,
            IStaffService staffService,
            IUserActionTokenService userActionTokenService,
            IUserOnboardingMailService userOnboardingMailService,
            ICurrentUser currentUser,
            IUserActionTokenHelper userActionTokenHelper,
            IOperationClaimService operationClaimService,
            IUserOperationClaimService userOperationClaimService
        )
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _tenantBusinessRules = tenantBusinessRules;
            _userService = userService;
            _tenantService = tenantService;
            _staffService = staffService;
            _currentUser = currentUser;
            _userActionTokenService = userActionTokenService;
            _userOnboardingMailService = userOnboardingMailService;
            _userActionTokenHelper = userActionTokenHelper;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        public async Task<CreatedTenantWithOwnerResponse> Handle(CreateWithOwnerTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant tenant = _mapper.Map<Tenant>(request);
            tenant = await _tenantService.AddAsync(tenant);

            User user = _mapper.Map<User>(request);
            user.Status = false;
            user.MustChangePassword = true;
            user = await _userService.AddAsync(user);

            OperationClaim? claim = await _operationClaimService.GetAsync(
                x => x.Name.Equals(GeneralOperationClaims.Owner),
                cancellationToken: cancellationToken
            );
            await _tenantBusinessRules.OperationClaimShouldExistWhenSelected(claim);

            UserOperationClaim userClaim = new UserOperationClaim(user.Id, claim!.Id);
            await _userOperationClaimService.AddAsync(userClaim);

            Staff staff = _mapper.Map<Staff>(request);
            staff.Role = Domain.Enums.StaffRole.Owner;
            staff.IsActive = false;
            staff.UserId = user.Id;
            staff.TenantId = tenant.Id;
            staff = await _staffService.AddAsync(staff);

            string token = _userActionTokenHelper.CreateActionToken();
            HashingHelper.CreateActionTokenHash(token, out byte[] tokenHash);

            DateTime utcNow = DateTime.UtcNow;
            UserActionToken userActionToken = new UserActionToken()
            {
                CreatedByUserId = _currentUser.UserId,
                UserId = user.Id,
                CreatedDate = utcNow,
                ExpiresAt = utcNow.Add(ExpirationTimes.ActivationLinkExpiration),
                Email = user.Email,
                Purpose = Domain.Enums.UserActionPurpose.AccountActivation,
                TargetEntityId = staff.Id,
                TargetType = Domain.Enums.UserActionTargetType.Owner,
                TenantId = tenant.Id,
                TokenHash = tokenHash
            };
            await _userActionTokenService.AddAsync(userActionToken);

            string activationLink = $"{LinkEndpoints.ActivationLinkEndpoint}?token={token}&email={user.Email}";
            await _userOnboardingMailService.SendOwnerInviteAsync(
                user.Email,
                staff.Name,
                tenant.Name,
                tenant.Subdomain,
                activationLink,
                cancellationToken
            );

            CreatedTenantWithOwnerResponse response = _mapper.Map<CreatedTenantWithOwnerResponse>(tenant);
            return response;
        }
    }
}
