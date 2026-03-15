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

namespace Application.Features.WorkoutTemplateDayExercises.Commands.Update;

public class UpdateWorkoutTemplateDayExerciseCommand
    : IRequest<UpdatedWorkoutTemplateDayExerciseResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public int Order { get; set; }
    public int Sets { get; set; }
    public string Reps { get; set; }
    public decimal? WeightKg { get; set; }
    public int? RestSeconds { get; set; }
    public string? Tempo { get; set; }
    public string? Note { get; set; }
    public int WorkoutTemplateDayId { get; set; }
    public int? ExerciseId { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplateDayExercisesOperationClaims.Update];

    public class UpdateWorkoutTemplateDayExerciseCommandHandler
        : IRequestHandler<UpdateWorkoutTemplateDayExerciseCommand, UpdatedWorkoutTemplateDayExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayExerciseRepository _workoutTemplateDayExerciseRepository;
        private readonly WorkoutTemplateDayExerciseBusinessRules _workoutTemplateDayExerciseBusinessRules;

        public UpdateWorkoutTemplateDayExerciseCommandHandler(
            IMapper mapper,
            IWorkoutTemplateDayExerciseRepository workoutTemplateDayExerciseRepository,
            WorkoutTemplateDayExerciseBusinessRules workoutTemplateDayExerciseBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayExerciseRepository = workoutTemplateDayExerciseRepository;
            _workoutTemplateDayExerciseBusinessRules = workoutTemplateDayExerciseBusinessRules;
        }

        public async Task<UpdatedWorkoutTemplateDayExerciseResponse> Handle(
            UpdateWorkoutTemplateDayExerciseCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDayExercise? workoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.GetAsync(
                predicate: wtde => wtde.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateDayExerciseBusinessRules.WorkoutTemplateDayExerciseShouldExistWhenSelected(workoutTemplateDayExercise);
            workoutTemplateDayExercise = _mapper.Map(request, workoutTemplateDayExercise);

            await _workoutTemplateDayExerciseRepository.UpdateAsync(workoutTemplateDayExercise!);

            UpdatedWorkoutTemplateDayExerciseResponse response = _mapper.Map<UpdatedWorkoutTemplateDayExerciseResponse>(
                workoutTemplateDayExercise
            );
            return response;
        }
    }
}
