using Application.Features.DietTemplateMeals.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.DietTemplateMeals.Rules;

public class DietTemplateMealBusinessRules : BaseBusinessRules
{
    private readonly IDietTemplateMealRepository _dietTemplateMealRepository;
    private readonly ILocalizationService _localizationService;

    public DietTemplateMealBusinessRules(IDietTemplateMealRepository dietTemplateMealRepository, ILocalizationService localizationService)
    {
        _dietTemplateMealRepository = dietTemplateMealRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DietTemplateMealsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DietTemplateMealShouldExistWhenSelected(DietTemplateMeal? dietTemplateMeal)
    {
        if (dietTemplateMeal == null)
            await throwBusinessException(DietTemplateMealsBusinessMessages.DietTemplateMealNotExists);
    }

    public async Task DietTemplateMealIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        DietTemplateMeal? dietTemplateMeal = await _dietTemplateMealRepository.GetAsync(
            predicate: dtm => dtm.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DietTemplateMealShouldExistWhenSelected(dietTemplateMeal);
    }
}
