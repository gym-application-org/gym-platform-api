using Application.Features.WorkoutTemplateDayExercises.Constants;
using Application.Features.WorkoutTemplateDayExercises.Constants;
using Application.Features.WorkoutTemplateDayExercises.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDayExercises.Constants.WorkoutTemplateDayExercisesOperationClaims;

namespace Application.Features.WorkoutTemplateDayExercises.Commands.Delete;

public class DeleteWorkoutTemplateDayExerciseCommand
    : IRequest<DeletedWorkoutTemplateDayExerciseResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplateDayExercisesOperationClaims.Delete];

    public class DeleteWorkoutTemplateDayExerciseCommandHandler
        : IRequestHandler<DeleteWorkoutTemplateDayExerciseCommand, DeletedWorkoutTemplateDayExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayExerciseRepository _workoutTemplateDayExerciseRepository;
        private readonly WorkoutTemplateDayExerciseBusinessRules _workoutTemplateDayExerciseBusinessRules;

        public DeleteWorkoutTemplateDayExerciseCommandHandler(
            IMapper mapper,
            IWorkoutTemplateDayExerciseRepository workoutTemplateDayExerciseRepository,
            WorkoutTemplateDayExerciseBusinessRules workoutTemplateDayExerciseBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayExerciseRepository = workoutTemplateDayExerciseRepository;
            _workoutTemplateDayExerciseBusinessRules = workoutTemplateDayExerciseBusinessRules;
        }

        public async Task<DeletedWorkoutTemplateDayExerciseResponse> Handle(
            DeleteWorkoutTemplateDayExerciseCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDayExercise? workoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.GetAsync(
                predicate: wtde => wtde.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateDayExerciseBusinessRules.WorkoutTemplateDayExerciseShouldExistWhenSelected(workoutTemplateDayExercise);

            await _workoutTemplateDayExerciseRepository.DeleteAsync(workoutTemplateDayExercise!);

            DeletedWorkoutTemplateDayExerciseResponse response = _mapper.Map<DeletedWorkoutTemplateDayExerciseResponse>(
                workoutTemplateDayExercise
            );
            return response;
        }
    }
}
