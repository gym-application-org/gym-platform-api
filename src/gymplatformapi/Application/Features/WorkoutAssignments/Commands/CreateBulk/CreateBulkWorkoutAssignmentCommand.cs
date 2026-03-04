using Application.Features.WorkoutAssignments.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using Application.Services.WorkoutTemplates;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.WorkoutAssignments.Commands.CreateBulk;

public class CreateBulkWorkoutAssignmentCommand
    : IRequest<CreatedBulkWorkoutAssignmentResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public int WorkoutTemplateId { get; set; }
    public ICollection<Guid> MemberIds { get; set; } = new List<Guid>();

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutAssignments"];

    public class CreateBulkWorkoutAssignmentCommandHandler
        : IRequestHandler<CreateBulkWorkoutAssignmentCommand, CreatedBulkWorkoutAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;
        private readonly ICurrentTenant _currentTenant;
        private readonly IMemberService _memberService;
        private readonly IWorkoutTemplateService _workoutTemplateService;

        public CreateBulkWorkoutAssignmentCommandHandler(
            IMapper mapper,
            IWorkoutAssignmentRepository workoutAssignmentRepository,
            WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules,
            ICurrentTenant currentTenant,
            IMemberService memberService,
            IWorkoutTemplateService workoutTemplateService
        )
        {
            _mapper = mapper;
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
            _currentTenant = currentTenant;
            _memberService = memberService;
            _workoutTemplateService = workoutTemplateService;
        }

        public async Task<CreatedBulkWorkoutAssignmentResponse> Handle(
            CreateBulkWorkoutAssignmentCommand request,
            CancellationToken cancellationToken
        )
        {
            Guid tenantId = _currentTenant.TenantId!.Value;
            List<Guid> distinctMemberIds = request.MemberIds.Distinct().ToList();

            var members = await _memberService.GetListAsync(
                predicate: x => distinctMemberIds.Contains(x.Id),
                index: 0,
                size: distinctMemberIds.Count,
                cancellationToken: cancellationToken
            );

            await _workoutAssignmentBusinessRules.AllMembersShouldExistInCurrentTenant(
                requestedMemberIds: distinctMemberIds,
                foundMembers: members?.Items
            );

            WorkoutTemplate? workoutTemplate = await _workoutTemplateService.GetAsync(
                x => x.Id == request.WorkoutTemplateId,
                cancellationToken: cancellationToken
            );
            await _workoutAssignmentBusinessRules.WorkoutTemplateShouldExistWhenSelected(workoutTemplate);

            List<WorkoutAssignment> assignments = new();

            foreach (Member member in members!.Items)
            {
                WorkoutAssignment dietAssignment =
                    new(
                        tenantId: tenantId,
                        memberId: member.Id,
                        workoutTemplateId: request.WorkoutTemplateId,
                        startDate: request.StartDate,
                        endDate: request.EndDate
                    )
                    {
                        Status = request.Status
                    };

                assignments.Add(dietAssignment);
            }

            await _workoutAssignmentRepository.AddRangeAsync(assignments);

            return new CreatedBulkWorkoutAssignmentResponse
            {
                WorkoutTemplateId = request.WorkoutTemplateId,
                CreatedCount = assignments.Count,
                AssignmentIds = assignments.Select(x => x.Id).ToList()
            };
        }
    }
}
