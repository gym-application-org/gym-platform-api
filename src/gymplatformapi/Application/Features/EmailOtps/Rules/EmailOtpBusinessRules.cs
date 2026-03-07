using Application.Features.EmailOtps.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.EmailOtps.Rules;

public class EmailOtpBusinessRules : BaseBusinessRules
{
    private readonly IEmailOtpRepository _emailOtpRepository;
    private readonly ILocalizationService _localizationService;

    public EmailOtpBusinessRules(IEmailOtpRepository emailOtpRepository, ILocalizationService localizationService)
    {
        _emailOtpRepository = emailOtpRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, EmailOtpsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task EmailOtpShouldExistWhenSelected(EmailOtp? emailOtp)
    {
        if (emailOtp == null)
            await throwBusinessException(EmailOtpsBusinessMessages.EmailOtpNotExists);
    }

    public async Task EmailOtpIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        EmailOtp? emailOtp = await _emailOtpRepository.GetAsync(
            predicate: eo => eo.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EmailOtpShouldExistWhenSelected(emailOtp);
    }
}
