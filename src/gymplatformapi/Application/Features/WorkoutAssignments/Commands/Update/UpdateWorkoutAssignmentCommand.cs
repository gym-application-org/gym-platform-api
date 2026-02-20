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

namespace Application.Features.WorkoutAssignments.Commands.Update;

public class UpdateWorkoutAssignmentCommand
    : IRequest<UpdatedWorkoutAssignmentResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public int WorkoutTemplateId { get; set; }

    public string[] Roles => [Admin, Write, WorkoutAssignmentsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutAssignments"];

    public class UpdateWorkoutAssignmentCommandHandler : IRequestHandler<UpdateWorkoutAssignmentCommand, UpdatedWorkoutAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;

        public UpdateWorkoutAssignmentCommandHandler(
            IMapper mapper,
            IWorkoutAssignmentRepository workoutAssignmentRepository,
            WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
        }

        public async Task<UpdatedWorkoutAssignmentResponse> Handle(
            UpdateWorkoutAssignmentCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutAssignment? workoutAssignment = await _workoutAssignmentRepository.GetAsync(
                predicate: wa => wa.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutAssignmentBusinessRules.WorkoutAssignmentShouldExistWhenSelected(workoutAssignment);
            workoutAssignment = _mapper.Map(request, workoutAssignment);

            await _workoutAssignmentRepository.UpdateAsync(workoutAssignment!);

            UpdatedWorkoutAssignmentResponse response = _mapper.Map<UpdatedWorkoutAssignmentResponse>(workoutAssignment);
            return response;
        }
    }
}
