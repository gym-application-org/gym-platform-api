using Application.Features.DietAssignments.Rules;
using Application.Features.WorkoutAssignments.Constants;
using Application.Features.WorkoutAssignments.Rules;
using Application.Services.DietTemplates;
using Application.Services.Members;
using Application.Services.Repositories;
using Application.Services.WorkoutTemplates;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Commands.Create;

public class CreateWorkoutAssignmentCommand
    : IRequest<CreatedWorkoutAssignmentResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public int WorkoutTemplateId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class CreateWorkoutAssignmentCommandHandler : IRequestHandler<CreateWorkoutAssignmentCommand, CreatedWorkoutAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;
        private readonly ICurrentTenant _currentTenant;
        private readonly IMemberService _memberService;
        private readonly IWorkoutTemplateService _workoutTemplateService;

        public CreateWorkoutAssignmentCommandHandler(
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

        public async Task<CreatedWorkoutAssignmentResponse> Handle(
            CreateWorkoutAssignmentCommand request,
            CancellationToken cancellationToken
        )
        {
            Member? member = await _memberService.GetAsync(x => x.Id == request.MemberId, cancellationToken: cancellationToken);
            await _workoutAssignmentBusinessRules.MemberShouldExistWhenSelected(member);

            WorkoutTemplate? workoutTemplate = await _workoutTemplateService.GetAsync(
                x => x.Id == request.WorkoutTemplateId,
                cancellationToken: cancellationToken
            );
            await _workoutAssignmentBusinessRules.WorkoutTemplateShouldExistWhenSelected(workoutTemplate);

            WorkoutAssignment workoutAssignment = _mapper.Map<WorkoutAssignment>(request);
            workoutAssignment.TenantId = _currentTenant.TenantId!.Value;

            await _workoutAssignmentRepository.AddAsync(workoutAssignment);

            CreatedWorkoutAssignmentResponse response = _mapper.Map<CreatedWorkoutAssignmentResponse>(workoutAssignment);
            return response;
        }
    }
}
