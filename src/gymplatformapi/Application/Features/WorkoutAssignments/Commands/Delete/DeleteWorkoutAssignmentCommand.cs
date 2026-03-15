using Application.Features.WorkoutAssignments.Constants;
using Application.Features.WorkoutAssignments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Commands.Delete;

public class DeleteWorkoutAssignmentCommand
    : IRequest<DeletedWorkoutAssignmentResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class DeleteWorkoutAssignmentCommandHandler : IRequestHandler<DeleteWorkoutAssignmentCommand, DeletedWorkoutAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;

        public DeleteWorkoutAssignmentCommandHandler(
            IMapper mapper,
            IWorkoutAssignmentRepository workoutAssignmentRepository,
            WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
        }

        public async Task<DeletedWorkoutAssignmentResponse> Handle(
            DeleteWorkoutAssignmentCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutAssignment? workoutAssignment = await _workoutAssignmentRepository.GetAsync(
                predicate: wa => wa.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutAssignmentBusinessRules.WorkoutAssignmentShouldExistWhenSelected(workoutAssignment);

            await _workoutAssignmentRepository.DeleteAsync(workoutAssignment!);

            DeletedWorkoutAssignmentResponse response = _mapper.Map<DeletedWorkoutAssignmentResponse>(workoutAssignment);
            return response;
        }
    }
}
