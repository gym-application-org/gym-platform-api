using Application.Features.WorkoutTemplateDayExercises.Constants;
using Application.Features.WorkoutTemplateDayExercises.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDayExercises.Constants.WorkoutTemplateDayExercisesOperationClaims;

namespace Application.Features.WorkoutTemplateDayExercises.Queries.GetById;

public class GetByIdWorkoutTemplateDayExerciseQuery : IRequest<GetByIdWorkoutTemplateDayExerciseResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdWorkoutTemplateDayExerciseQueryHandler
        : IRequestHandler<GetByIdWorkoutTemplateDayExerciseQuery, GetByIdWorkoutTemplateDayExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayExerciseRepository _workoutTemplateDayExerciseRepository;
        private readonly WorkoutTemplateDayExerciseBusinessRules _workoutTemplateDayExerciseBusinessRules;

        public GetByIdWorkoutTemplateDayExerciseQueryHandler(
            IMapper mapper,
            IWorkoutTemplateDayExerciseRepository workoutTemplateDayExerciseRepository,
            WorkoutTemplateDayExerciseBusinessRules workoutTemplateDayExerciseBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayExerciseRepository = workoutTemplateDayExerciseRepository;
            _workoutTemplateDayExerciseBusinessRules = workoutTemplateDayExerciseBusinessRules;
        }

        public async Task<GetByIdWorkoutTemplateDayExerciseResponse> Handle(
            GetByIdWorkoutTemplateDayExerciseQuery request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDayExercise? workoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.GetAsync(
                predicate: wtde => wtde.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateDayExerciseBusinessRules.WorkoutTemplateDayExerciseShouldExistWhenSelected(workoutTemplateDayExercise);

            GetByIdWorkoutTemplateDayExerciseResponse response = _mapper.Map<GetByIdWorkoutTemplateDayExerciseResponse>(
                workoutTemplateDayExercise
            );
            return response;
        }
    }
}
