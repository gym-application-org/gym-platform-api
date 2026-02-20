using Application.Features.WorkoutAssignments.Constants;
using Application.Features.WorkoutAssignments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Commands.Create;

public class CreateWorkoutAssignmentCommand
    : IRequest<CreatedWorkoutAssignmentResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public int WorkoutTemplateId { get; set; }

    public string[] Roles => [Admin, Write, WorkoutAssignmentsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutAssignments"];

    public class CreateWorkoutAssignmentCommandHandler : IRequestHandler<CreateWorkoutAssignmentCommand, CreatedWorkoutAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;

        public CreateWorkoutAssignmentCommandHandler(
            IMapper mapper,
            IWorkoutAssignmentRepository workoutAssignmentRepository,
            WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
        }

        public async Task<CreatedWorkoutAssignmentResponse> Handle(
            CreateWorkoutAssignmentCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutAssignment workoutAssignment = _mapper.Map<WorkoutAssignment>(request);

            await _workoutAssignmentRepository.AddAsync(workoutAssignment);

            CreatedWorkoutAssignmentResponse response = _mapper.Map<CreatedWorkoutAssignmentResponse>(workoutAssignment);
            return response;
        }
    }
}
