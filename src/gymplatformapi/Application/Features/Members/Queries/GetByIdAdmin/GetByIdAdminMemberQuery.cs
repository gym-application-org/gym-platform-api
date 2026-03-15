using Application.Features.Members.Constants;
using Application.Features.Members.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Members.Constants.MembersOperationClaims;

namespace Application.Features.Members.Queries.GetByIdAdmin;

public class GetByIdAdminMemberQuery : IRequest<GetByIdAdminMemberResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class GetByIdMemberQueryHandler : IRequestHandler<GetByIdAdminMemberQuery, GetByIdAdminMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public GetByIdMemberQueryHandler(IMapper mapper, IMemberRepository memberRepository, MemberBusinessRules memberBusinessRules)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<GetByIdAdminMemberResponse> Handle(GetByIdAdminMemberQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetAsync(
                predicate: m => m.Id == request.Id,
                withDeleted: true,
                cancellationToken: cancellationToken
            );
            await _memberBusinessRules.MemberShouldExistWhenSelected(member);

            GetByIdAdminMemberResponse response = _mapper.Map<GetByIdAdminMemberResponse>(member);
            return response;
        }
    }
}
