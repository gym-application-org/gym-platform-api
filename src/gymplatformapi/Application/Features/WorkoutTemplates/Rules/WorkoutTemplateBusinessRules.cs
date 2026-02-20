using Application.Features.WorkoutTemplates.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.WorkoutTemplates.Rules;

public class WorkoutTemplateBusinessRules : BaseBusinessRules
{
    private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
    private readonly ILocalizationService _localizationService;

    public WorkoutTemplateBusinessRules(IWorkoutTemplateRepository workoutTemplateRepository, ILocalizationService localizationService)
    {
        _workoutTemplateRepository = workoutTemplateRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WorkoutTemplatesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WorkoutTemplateShouldExistWhenSelected(WorkoutTemplate? workoutTemplate)
    {
        if (workoutTemplate == null)
            await throwBusinessException(WorkoutTemplatesBusinessMessages.WorkoutTemplateNotExists);
    }

    public async Task WorkoutTemplateIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        WorkoutTemplate? workoutTemplate = await _workoutTemplateRepository.GetAsync(
            predicate: wt => wt.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WorkoutTemplateShouldExistWhenSelected(workoutTemplate);
    }
}
