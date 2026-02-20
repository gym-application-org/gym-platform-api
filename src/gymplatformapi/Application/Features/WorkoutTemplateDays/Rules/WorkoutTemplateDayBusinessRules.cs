using Application.Features.WorkoutTemplateDays.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.WorkoutTemplateDays.Rules;

public class WorkoutTemplateDayBusinessRules : BaseBusinessRules
{
    private readonly IWorkoutTemplateDayRepository _workoutTemplateDayRepository;
    private readonly ILocalizationService _localizationService;

    public WorkoutTemplateDayBusinessRules(
        IWorkoutTemplateDayRepository workoutTemplateDayRepository,
        ILocalizationService localizationService
    )
    {
        _workoutTemplateDayRepository = workoutTemplateDayRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WorkoutTemplateDaysBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WorkoutTemplateDayShouldExistWhenSelected(WorkoutTemplateDay? workoutTemplateDay)
    {
        if (workoutTemplateDay == null)
            await throwBusinessException(WorkoutTemplateDaysBusinessMessages.WorkoutTemplateDayNotExists);
    }

    public async Task WorkoutTemplateDayIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        WorkoutTemplateDay? workoutTemplateDay = await _workoutTemplateDayRepository.GetAsync(
            predicate: wtd => wtd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WorkoutTemplateDayShouldExistWhenSelected(workoutTemplateDay);
    }
}
