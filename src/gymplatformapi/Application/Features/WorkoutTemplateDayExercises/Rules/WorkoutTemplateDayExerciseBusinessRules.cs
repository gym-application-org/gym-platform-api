using Application.Features.WorkoutTemplateDayExercises.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.WorkoutTemplateDayExercises.Rules;

public class WorkoutTemplateDayExerciseBusinessRules : BaseBusinessRules
{
    private readonly IWorkoutTemplateDayExerciseRepository _workoutTemplateDayExerciseRepository;
    private readonly ILocalizationService _localizationService;

    public WorkoutTemplateDayExerciseBusinessRules(
        IWorkoutTemplateDayExerciseRepository workoutTemplateDayExerciseRepository,
        ILocalizationService localizationService
    )
    {
        _workoutTemplateDayExerciseRepository = workoutTemplateDayExerciseRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WorkoutTemplateDayExercisesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WorkoutTemplateDayExerciseShouldExistWhenSelected(WorkoutTemplateDayExercise? workoutTemplateDayExercise)
    {
        if (workoutTemplateDayExercise == null)
            await throwBusinessException(WorkoutTemplateDayExercisesBusinessMessages.WorkoutTemplateDayExerciseNotExists);
    }

    public async Task WorkoutTemplateDayExerciseIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        WorkoutTemplateDayExercise? workoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.GetAsync(
            predicate: wtde => wtde.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WorkoutTemplateDayExerciseShouldExistWhenSelected(workoutTemplateDayExercise);
    }
}
