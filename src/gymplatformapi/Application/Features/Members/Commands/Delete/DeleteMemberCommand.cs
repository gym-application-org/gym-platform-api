using Application.Features.Members.Constants;
using Application.Features.Members.Constants;
using Application.Features.Members.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using Application.Services.UsersService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using static Application.Features.Members.Constants.MembersOperationClaims;

namespace Application.Features.Members.Commands.Delete;

public class DeleteMemberCommand
    : IRequest<DeletedMemberResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetMembers"];

    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, DeletedMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;
        private readonly IUserService _userService;

        public DeleteMemberCommandHandler(
            IMapper mapper,
            IMemberRepository memberRepository,
            MemberBusinessRules memberBusinessRules,
            IUserService userService
        )
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
            _userService = userService;
        }

        public async Task<DeletedMemberResponse> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _memberBusinessRules.MemberShouldExistWhenSelected(member);

            User? user = await _userService.GetAsync(predicate: x => x.Id == member!.UserId, cancellationToken: cancellationToken);

            await _memberRepository.DeleteAsync(member!);
            await _userService.DeleteAsync(user!);

            DeletedMemberResponse response = _mapper.Map<DeletedMemberResponse>(member);
            return response;
        }
    }
}
