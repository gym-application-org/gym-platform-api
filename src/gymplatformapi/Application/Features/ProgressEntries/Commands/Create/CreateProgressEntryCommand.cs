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

namespace Application.Features.ProgressEntries.Commands.Create;

public class CreateProgressEntryCommand
    : IRequest<CreatedProgressEntryResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
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

    public class CreateProgressEntryCommandHandler : IRequestHandler<CreateProgressEntryCommand, CreatedProgressEntryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProgressEntryRepository _progressEntryRepository;
        private readonly ProgressEntryBusinessRules _progressEntryBusinessRules;
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentTenant _currentTenant;
        private readonly IMemberService _memberService;

        public CreateProgressEntryCommandHandler(
            IMapper mapper,
            IProgressEntryRepository progressEntryRepository,
            ProgressEntryBusinessRules progressEntryBusinessRules,
            ICurrentUser currentUser,
            ICurrentTenant currentTenant,
            IMemberService memberService
        )
        {
            _mapper = mapper;
            _progressEntryRepository = progressEntryRepository;
            _progressEntryBusinessRules = progressEntryBusinessRules;
            _currentUser = currentUser;
            _memberService = memberService;
            _currentTenant = currentTenant;
        }

        public async Task<CreatedProgressEntryResponse> Handle(CreateProgressEntryCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(
                predicate: x => x.UserId == _currentUser.UserId,
                cancellationToken: cancellationToken
            );
            await _progressEntryBusinessRules.MemberShouldExistWhenSelected(member);

            ProgressEntry progressEntry = _mapper.Map<ProgressEntry>(request);
            progressEntry.MemberId = member!.Id;
            progressEntry.TenantId = _currentTenant.TenantId!.Value;

            await _progressEntryRepository.AddAsync(progressEntry);

            CreatedProgressEntryResponse response = _mapper.Map<CreatedProgressEntryResponse>(progressEntry);
            return response;
        }
    }
}
