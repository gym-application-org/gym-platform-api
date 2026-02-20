using Application.Features.Exercises.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Exercises.Rules;

public class ExerciseBusinessRules : BaseBusinessRules
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ILocalizationService _localizationService;

    public ExerciseBusinessRules(IExerciseRepository exerciseRepository, ILocalizationService localizationService)
    {
        _exerciseRepository = exerciseRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ExercisesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ExerciseShouldExistWhenSelected(Exercise? exercise)
    {
        if (exercise == null)
            await throwBusinessException(ExercisesBusinessMessages.ExerciseNotExists);
    }

    public async Task ExerciseIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Exercise? exercise = await _exerciseRepository.GetAsync(
            predicate: e => e.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ExerciseShouldExistWhenSelected(exercise);
    }
}
