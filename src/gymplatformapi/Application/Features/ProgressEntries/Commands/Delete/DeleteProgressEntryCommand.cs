using Application.Features.ProgressEntries.Constants;
using Application.Features.ProgressEntries.Constants;
using Application.Features.ProgressEntries.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.ProgressEntries.Constants.ProgressEntriesOperationClaims;

namespace Application.Features.ProgressEntries.Commands.Delete;

public class DeleteProgressEntryCommand
    : IRequest<DeletedProgressEntryResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class DeleteProgressEntryCommandHandler : IRequestHandler<DeleteProgressEntryCommand, DeletedProgressEntryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProgressEntryRepository _progressEntryRepository;
        private readonly ProgressEntryBusinessRules _progressEntryBusinessRules;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;

        public DeleteProgressEntryCommandHandler(
            IMapper mapper,
            IProgressEntryRepository progressEntryRepository,
            ProgressEntryBusinessRules progressEntryBusinessRules,
            ICurrentUser currentUser,
            IMemberService memberService
        )
        {
            _mapper = mapper;
            _progressEntryRepository = progressEntryRepository;
            _progressEntryBusinessRules = progressEntryBusinessRules;
            _currentUser = currentUser;
            _memberService = memberService;
        }

        public async Task<DeletedProgressEntryResponse> Handle(DeleteProgressEntryCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(
                predicate: x => x.UserId == _currentUser.UserId,
                cancellationToken: cancellationToken
            );
            await _progressEntryBusinessRules.MemberShouldExistWhenSelected(member);

            ProgressEntry? progressEntry = await _progressEntryRepository.GetAsync(
                predicate: pe => pe.Id == request.Id && pe.MemberId == member!.Id,
                cancellationToken: cancellationToken
            );
            await _progressEntryBusinessRules.ProgressEntryShouldExistWhenSelected(progressEntry);

            await _progressEntryRepository.DeleteAsync(progressEntry!);

            DeletedProgressEntryResponse response = _mapper.Map<DeletedProgressEntryResponse>(progressEntry);
            return response;
        }
    }
}
