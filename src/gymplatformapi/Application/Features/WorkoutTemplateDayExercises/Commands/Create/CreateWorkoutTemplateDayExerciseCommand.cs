using Application.Features.WorkoutTemplateDayExercises.Constants;
using Application.Features.WorkoutTemplateDayExercises.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDayExercises.Constants.WorkoutTemplateDayExercisesOperationClaims;

namespace Application.Features.WorkoutTemplateDayExercises.Commands.Create;

public class CreateWorkoutTemplateDayExerciseCommand
    : IRequest<CreatedWorkoutTemplateDayExerciseResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Order { get; set; }
    public int Sets { get; set; }
    public string Reps { get; set; }
    public decimal? WeightKg { get; set; }
    public int? RestSeconds { get; set; }
    public string? Tempo { get; set; }
    public string? Note { get; set; }
    public int WorkoutTemplateDayId { get; set; }
    public int? ExerciseId { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplateDayExercisesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutTemplateDayExercises"];

    public class CreateWorkoutTemplateDayExerciseCommandHandler
        : IRequestHandler<CreateWorkoutTemplateDayExerciseCommand, CreatedWorkoutTemplateDayExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayExerciseRepository _workoutTemplateDayExerciseRepository;
        private readonly WorkoutTemplateDayExerciseBusinessRules _workoutTemplateDayExerciseBusinessRules;

        public CreateWorkoutTemplateDayExerciseCommandHandler(
            IMapper mapper,
            IWorkoutTemplateDayExerciseRepository workoutTemplateDayExerciseRepository,
            WorkoutTemplateDayExerciseBusinessRules workoutTemplateDayExerciseBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayExerciseRepository = workoutTemplateDayExerciseRepository;
            _workoutTemplateDayExerciseBusinessRules = workoutTemplateDayExerciseBusinessRules;
        }

        public async Task<CreatedWorkoutTemplateDayExerciseResponse> Handle(
            CreateWorkoutTemplateDayExerciseCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDayExercise workoutTemplateDayExercise = _mapper.Map<WorkoutTemplateDayExercise>(request);

            await _workoutTemplateDayExerciseRepository.AddAsync(workoutTemplateDayExercise);

            CreatedWorkoutTemplateDayExerciseResponse response = _mapper.Map<CreatedWorkoutTemplateDayExerciseResponse>(
                workoutTemplateDayExercise
            );
            return response;
        }
    }
}
