using Application.Features.Members.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Members.Constants.MembersOperationClaims;

namespace Application.Features.Members.Queries.GetListAdmin;

public class GetListAdminMemberQuery : IRequest<GetListResponse<GetListAdminMemberListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public MemberStatus? Status { get; set; }

    public Guid? TenantId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class GetListMemberQueryHandler : IRequestHandler<GetListAdminMemberQuery, GetListResponse<GetListAdminMemberListItemDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetListMemberQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAdminMemberListItemDto>> Handle(
            GetListAdminMemberQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Member> members = await _memberRepository.GetListAsync(
                predicate: x =>
                    (!request.Status.HasValue || request.Status == x.Status)
                    && (!request.TenantId.HasValue || request.TenantId == x.TenantId),
                ignoreQueryFilters: true,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAdminMemberListItemDto> response = _mapper.Map<GetListResponse<GetListAdminMemberListItemDto>>(members);
            return response;
        }
    }
}
