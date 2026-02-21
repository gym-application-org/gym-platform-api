using Application.Features.ProgressEntries.Constants;
using Application.Features.ProgressEntries.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.ProgressEntries.Constants.ProgressEntriesOperationClaims;

namespace Application.Features.ProgressEntries.Commands.Update;

public class UpdateProgressEntryCommand
    : IRequest<UpdatedProgressEntryResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? BodyFatPercent { get; set; }
    public decimal? MuscleMassKg { get; set; }
    public decimal? ChestCm { get; set; }
    public decimal? WaistCm { get; set; }
    public decimal? HipCm { get; set; }
    public decimal? ArmCm { get; set; }
    public decimal? LegCm { get; set; }
    public string? Note { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProgressEntries"];

    public class UpdateProgressEntryCommandHandler : IRequestHandler<UpdateProgressEntryCommand, UpdatedProgressEntryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProgressEntryRepository _progressEntryRepository;
        private readonly ProgressEntryBusinessRules _progressEntryBusinessRules;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;

        public UpdateProgressEntryCommandHandler(
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

        public async Task<UpdatedProgressEntryResponse> Handle(UpdateProgressEntryCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(
                predicate: x => x.UserId == _currentUser.UserId,
                cancellationToken: cancellationToken
            );
            await _progressEntryBusinessRules.MemberShouldExistWhenSelected(member);

            ProgressEntry? progressEntry = await _progressEntryRepository.GetAsync(
                predicate: pe => pe.Id == request.Id && member!.Id == pe.MemberId,
                cancellationToken: cancellationToken
            );
            await _progressEntryBusinessRules.ProgressEntryShouldExistWhenSelected(progressEntry);
            progressEntry = _mapper.Map(request, progressEntry);

            await _progressEntryRepository.UpdateAsync(progressEntry!);

            UpdatedProgressEntryResponse response = _mapper.Map<UpdatedProgressEntryResponse>(progressEntry);
            return response;
        }
    }
}
