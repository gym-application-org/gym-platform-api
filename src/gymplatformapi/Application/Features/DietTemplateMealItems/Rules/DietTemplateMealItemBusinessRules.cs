using Application.Features.DietTemplateMealItems.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.DietTemplateMealItems.Rules;

public class DietTemplateMealItemBusinessRules : BaseBusinessRules
{
    private readonly IDietTemplateMealItemRepository _dietTemplateMealItemRepository;
    private readonly ILocalizationService _localizationService;

    public DietTemplateMealItemBusinessRules(
        IDietTemplateMealItemRepository dietTemplateMealItemRepository,
        ILocalizationService localizationService
    )
    {
        _dietTemplateMealItemRepository = dietTemplateMealItemRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DietTemplateMealItemsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DietTemplateMealItemShouldExistWhenSelected(DietTemplateMealItem? dietTemplateMealItem)
    {
        if (dietTemplateMealItem == null)
            await throwBusinessException(DietTemplateMealItemsBusinessMessages.DietTemplateMealItemNotExists);
    }

    public async Task DietTemplateMealItemIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        DietTemplateMealItem? dietTemplateMealItem = await _dietTemplateMealItemRepository.GetAsync(
            predicate: dtmi => dtmi.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DietTemplateMealItemShouldExistWhenSelected(dietTemplateMealItem);
    }
}
