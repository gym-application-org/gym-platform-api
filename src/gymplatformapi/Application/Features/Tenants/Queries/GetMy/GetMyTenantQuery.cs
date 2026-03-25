using Application.Features.Tenants.Constants;
using Application.Features.Tenants.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using Application.Services.Staffs;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Tenants.Constants.TenantsOperationClaims;

namespace Application.Features.Tenants.Queries.GetMy;

public class GetMyTenantQuery : IRequest<GetMyTenantResponse>, ISecuredRequest
{
    public string[] Roles => [GeneralOperationClaims.Member, GeneralOperationClaims.Owner, GeneralOperationClaims.Staff];

    public class GetByIdTenantQueryHandler : IRequestHandler<GetMyTenantQuery, GetMyTenantResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITenantRepository _tenantRepository;
        private readonly TenantBusinessRules _tenantBusinessRules;
        private ICurrentUser _currentUser;
        private IMemberService _memberService;
        private IStaffService _staffService;

        public GetByIdTenantQueryHandler(
            IMapper mapper,
            ITenantRepository tenantRepository,
            TenantBusinessRules tenantBusinessRules,
            IMemberService memberService,
            IStaffService staffService,
            ICurrentUser currentUser
        )
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _tenantBusinessRules = tenantBusinessRules;
            _currentUser = currentUser;
            _staffService = staffService;
            _memberService = memberService;
        }

        public async Task<GetMyTenantResponse> Handle(GetMyTenantQuery request, CancellationToken cancellationToken)
        {
            Guid? tenantId = null;

            Member? member = await _memberService.GetAsync(
                x => x.UserId == _currentUser.UserId,
                withDeleted: true,
                cancellationToken: cancellationToken
            );
            if (member == null)
            {
                Staff? staff = await _staffService.GetAsync(
                    x => x.UserId == _currentUser.UserId,
                    withDeleted: true,
                    cancellationToken: cancellationToken
                );
                if (staff == null)
                {
                    await _tenantBusinessRules.throwTenantMemberShouldExist();
                }
                else
                {
                    await _tenantBusinessRules.StaffShouldBeActivated(staff);
                    tenantId = staff!.TenantId;
                }
            }
            else
            {
                await _tenantBusinessRules.MemberShouldBeActivated(member);
                tenantId = member!.TenantId;
            }

            Tenant? tenant = await _tenantRepository.GetAsync(
                predicate: t => t.Id == tenantId,
                withDeleted: true,
                cancellationToken: cancellationToken
            );
            await _tenantBusinessRules.TenantShouldExistWhenSelected(tenant);

            GetMyTenantResponse response = _mapper.Map<GetMyTenantResponse>(tenant);
            return response;
        }
    }
}
