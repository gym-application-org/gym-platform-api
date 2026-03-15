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

namespace Application.Features.Members.Queries.GetList;

public class GetListMemberQuery : IRequest<GetListResponse<GetListMemberListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public MemberStatus? Status { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetListMemberQueryHandler : IRequestHandler<GetListMemberQuery, GetListResponse<GetListMemberListItemDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetListMemberQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMemberListItemDto>> Handle(GetListMemberQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Member> members = await _memberRepository.GetListAsync(
                predicate: x => (!request.Status.HasValue || request.Status == x.Status),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMemberListItemDto> response = _mapper.Map<GetListResponse<GetListMemberListItemDto>>(members);
            return response;
        }
    }
}
