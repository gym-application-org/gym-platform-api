using Application.Features.DietTemplateDays.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.DietTemplateDays.Rules;

public class DietTemplateDayBusinessRules : BaseBusinessRules
{
    private readonly IDietTemplateDayRepository _dietTemplateDayRepository;
    private readonly ILocalizationService _localizationService;

    public DietTemplateDayBusinessRules(IDietTemplateDayRepository dietTemplateDayRepository, ILocalizationService localizationService)
    {
        _dietTemplateDayRepository = dietTemplateDayRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DietTemplateDaysBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DietTemplateDayShouldExistWhenSelected(DietTemplateDay? dietTemplateDay)
    {
        if (dietTemplateDay == null)
            await throwBusinessException(DietTemplateDaysBusinessMessages.DietTemplateDayNotExists);
    }

    public async Task DietTemplateDayIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        DietTemplateDay? dietTemplateDay = await _dietTemplateDayRepository.GetAsync(
            predicate: dtd => dtd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DietTemplateDayShouldExistWhenSelected(dietTemplateDay);
    }
}
