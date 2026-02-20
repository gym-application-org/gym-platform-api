using Application.Features.SupportTickets.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.SupportTickets.Rules;

public class SupportTicketBusinessRules : BaseBusinessRules
{
    private readonly ISupportTicketRepository _supportTicketRepository;
    private readonly ILocalizationService _localizationService;

    public SupportTicketBusinessRules(ISupportTicketRepository supportTicketRepository, ILocalizationService localizationService)
    {
        _supportTicketRepository = supportTicketRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SupportTicketsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SupportTicketShouldExistWhenSelected(SupportTicket? supportTicket)
    {
        if (supportTicket == null)
            await throwBusinessException(SupportTicketsBusinessMessages.SupportTicketNotExists);
    }

    public async Task SupportTicketIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        SupportTicket? supportTicket = await _supportTicketRepository.GetAsync(
            predicate: st => st.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SupportTicketShouldExistWhenSelected(supportTicket);
    }
}
