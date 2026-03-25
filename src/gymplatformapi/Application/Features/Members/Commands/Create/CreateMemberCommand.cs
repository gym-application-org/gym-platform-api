using Application.Constants;
using Application.Features.Members.Constants;
using Application.Features.Members.Rules;
using Application.Features.Staffs.Rules;
using Application.Features.Tenants.Constants;
using Application.Services.MailServices.UserOnboardingMails;
using Application.Services.Members;
using Application.Services.OperationClaims;
using Application.Services.Repositories;
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
using static Application.Features.Members.Constants.MembersOperationClaims;

namespace Application.Features.Members.Commands.Create;

public class CreateMemberCommand : IRequest<CreatedMemberResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest, ITenantRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner, GeneralOperationClaims.Staff];

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, CreatedMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentTenant _currentTenant;
        private readonly IMemberService _memberService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserActionTokenHelper _userActionTokenHelper;
        private readonly IUserActionTokenService _userActionTokenService;
        private readonly IUserOnboardingMailService _userOnboardingMailService;
        private readonly ITenantService _tenantService;

        public CreateMemberCommandHandler(
            IMapper mapper,
            IMemberRepository memberRepository,
            MemberBusinessRules memberBusinessRules,
            IUserService userService,
            IMemberService memberService,
            IUserOperationClaimService userOperationClaimService,
            IOperationClaimService operationClaimService,
            IUserActionTokenService userActionTokenService,
            IUserActionTokenHelper userActionTokenHelper,
            ICurrentTenant currentTenant,
            ICurrentUser currentUser,
            IUserOnboardingMailService userOnboardingMailService,
            ITenantService tenantService
        )
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
            _userService = userService;
            _memberService = memberService;
            _currentUser = currentUser;
            _currentTenant = currentTenant;
            _userActionTokenHelper = userActionTokenHelper;
            _userActionTokenService = userActionTokenService;
            _userOnboardingMailService = userOnboardingMailService;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
            _tenantService = tenantService;
        }

        public async Task<CreatedMemberResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            Guid tenantId = _currentTenant.TenantId!.Value;
            int userId = _currentUser.UserId;

            Tenant? tenant = await _tenantService.GetAsync(x => x.Id == tenantId, cancellationToken: cancellationToken);
            await _memberBusinessRules.TenantShouldExistWhenSelected(tenant);

            User user = _mapper.Map<User>(request);
            user.Status = false;
            user.MustChangePassword = true;
            user = await _userService.AddAsync(user);

            OperationClaim? claim = await _operationClaimService.GetAsync(
                x => x.Name.Equals(GeneralOperationClaims.Member),
                cancellationToken: cancellationToken
            );
            await _memberBusinessRules.OperationClaimShouldExistWhenSelected(claim);

            UserOperationClaim userClaim = new UserOperationClaim(user.Id, claim!.Id);
            await _userOperationClaimService.AddAsync(userClaim);

            Member member = _mapper.Map<Member>(request);
            member.Status = MemberStatus.Pending;
            member.UserId = user.Id;
            member.TenantId = tenantId;
            await _memberRepository.AddAsync(member);

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
                TargetEntityId = member.Id,
                TargetType = Domain.Enums.UserActionTargetType.Member,
                TenantId = tenantId,
                TokenHash = tokenHash
            };
            await _userActionTokenService.AddAsync(userActionToken);

            string activationLink = $"{LinkEndpoints.ActivationLinkEndpoint}?token={token}&email={user.Email}";
            await _userOnboardingMailService.SendMemberInviteAsync(
                user.Email,
                member.FirstName + " " + member.LastName,
                tenant!.Name,
                tenant.Subdomain,
                activationLink,
                cancellationToken
            );

            CreatedMemberResponse response = _mapper.Map<CreatedMemberResponse>(member);
            return response;
        }
    }
}
