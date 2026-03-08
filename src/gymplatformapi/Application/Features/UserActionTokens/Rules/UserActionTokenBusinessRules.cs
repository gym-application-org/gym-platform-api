using Application.Features.UserActionTokens.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.UserActionTokens.Rules;

public class UserActionTokenBusinessRules : BaseBusinessRules
{
    private readonly IUserActionTokenRepository _userActionTokenRepository;
    private readonly ILocalizationService _localizationService;

    public UserActionTokenBusinessRules(IUserActionTokenRepository userActionTokenRepository, ILocalizationService localizationService)
    {
        _userActionTokenRepository = userActionTokenRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UserActionTokensBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task UserActionTokenShouldExistWhenSelected(UserActionToken? userActionToken)
    {
        if (userActionToken == null)
            await throwBusinessException(UserActionTokensBusinessMessages.UserActionTokenNotExists);
    }

    public async Task UserActionTokenIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        UserActionToken? userActionToken = await _userActionTokenRepository.GetAsync(
            predicate: uat => uat.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await UserActionTokenShouldExistWhenSelected(userActionToken);
    }
}
