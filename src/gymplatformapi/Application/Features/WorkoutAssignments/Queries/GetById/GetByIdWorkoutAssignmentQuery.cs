using Application.Features.WorkoutAssignments.Constants;
using Application.Features.WorkoutAssignments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Queries.GetById;

public class GetByIdWorkoutAssignmentQuery : IRequest<GetByIdWorkoutAssignmentResponse>, ISecuredRequest, ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetByIdWorkoutAssignmentQueryHandler : IRequestHandler<GetByIdWorkoutAssignmentQuery, GetByIdWorkoutAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;

        public GetByIdWorkoutAssignmentQueryHandler(
            IMapper mapper,
            IWorkoutAssignmentRepository workoutAssignmentRepository,
            WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
        }

        public async Task<GetByIdWorkoutAssignmentResponse> Handle(
            GetByIdWorkoutAssignmentQuery request,
            CancellationToken cancellationToken
        )
        {
            WorkoutAssignment? workoutAssignment = await _workoutAssignmentRepository.GetAsync(
                predicate: wa => wa.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutAssignmentBusinessRules.WorkoutAssignmentShouldExistWhenSelected(workoutAssignment);

            GetByIdWorkoutAssignmentResponse response = _mapper.Map<GetByIdWorkoutAssignmentResponse>(workoutAssignment);
            return response;
        }
    }
}
