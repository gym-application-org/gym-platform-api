using Application.Features.Members.Constants;
using Application.Features.Members.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Members.Constants.MembersOperationClaims;

namespace Application.Features.Members.Commands.Create;

public class CreateMemberCommand
    : IRequest<CreatedMemberResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public MemberStatus Status { get; set; }
    public int UserId { get; set; }

    public string[] Roles => [Admin, Write, MembersOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetMembers"];

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, CreatedMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public CreateMemberCommandHandler(IMapper mapper, IMemberRepository memberRepository, MemberBusinessRules memberBusinessRules)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<CreatedMemberResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            Member member = _mapper.Map<Member>(request);

            await _memberRepository.AddAsync(member);

            CreatedMemberResponse response = _mapper.Map<CreatedMemberResponse>(member);
            return response;
        }
    }
}
