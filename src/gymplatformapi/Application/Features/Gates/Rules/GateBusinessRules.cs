using Application.Features.Gates.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Gates.Rules;

public class GateBusinessRules : BaseBusinessRules
{
    private readonly IGateRepository _gateRepository;
    private readonly ILocalizationService _localizationService;

    public GateBusinessRules(IGateRepository gateRepository, ILocalizationService localizationService)
    {
        _gateRepository = gateRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, GatesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task GateShouldExistWhenSelected(Gate? gate)
    {
        if (gate == null)
            await throwBusinessException(GatesBusinessMessages.GateNotExists);
    }

    public async Task GateIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Gate? gate = await _gateRepository.GetAsync(
            predicate: g => g.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await GateShouldExistWhenSelected(gate);
    }
}
