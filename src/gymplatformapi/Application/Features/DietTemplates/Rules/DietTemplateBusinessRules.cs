using Application.Features.DietTemplates.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.DietTemplates.Rules;

public class DietTemplateBusinessRules : BaseBusinessRules
{
    private readonly IDietTemplateRepository _dietTemplateRepository;
    private readonly ILocalizationService _localizationService;

    public DietTemplateBusinessRules(IDietTemplateRepository dietTemplateRepository, ILocalizationService localizationService)
    {
        _dietTemplateRepository = dietTemplateRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DietTemplatesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DietTemplateShouldExistWhenSelected(DietTemplate? dietTemplate)
    {
        if (dietTemplate == null)
            await throwBusinessException(DietTemplatesBusinessMessages.DietTemplateNotExists);
    }

    public async Task DietTemplateIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        DietTemplate? dietTemplate = await _dietTemplateRepository.GetAsync(
            predicate: dt => dt.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DietTemplateShouldExistWhenSelected(dietTemplate);
    }
}
