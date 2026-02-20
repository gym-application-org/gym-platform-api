using Application.Features.ProgressEntries.Constants;
using Application.Features.ProgressEntries.Constants;
using Application.Features.ProgressEntries.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.ProgressEntries.Constants.ProgressEntriesOperationClaims;

namespace Application.Features.ProgressEntries.Commands.Delete;

public class DeleteProgressEntryCommand
    : IRequest<DeletedProgressEntryResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ProgressEntriesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProgressEntries"];

    public class DeleteProgressEntryCommandHandler : IRequestHandler<DeleteProgressEntryCommand, DeletedProgressEntryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProgressEntryRepository _progressEntryRepository;
        private readonly ProgressEntryBusinessRules _progressEntryBusinessRules;

        public DeleteProgressEntryCommandHandler(
            IMapper mapper,
            IProgressEntryRepository progressEntryRepository,
            ProgressEntryBusinessRules progressEntryBusinessRules
        )
        {
            _mapper = mapper;
            _progressEntryRepository = progressEntryRepository;
            _progressEntryBusinessRules = progressEntryBusinessRules;
        }

        public async Task<DeletedProgressEntryResponse> Handle(DeleteProgressEntryCommand request, CancellationToken cancellationToken)
        {
            ProgressEntry? progressEntry = await _progressEntryRepository.GetAsync(
                predicate: pe => pe.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _progressEntryBusinessRules.ProgressEntryShouldExistWhenSelected(progressEntry);

            await _progressEntryRepository.DeleteAsync(progressEntry!);

            DeletedProgressEntryResponse response = _mapper.Map<DeletedProgressEntryResponse>(progressEntry);
            return response;
        }
    }
}
