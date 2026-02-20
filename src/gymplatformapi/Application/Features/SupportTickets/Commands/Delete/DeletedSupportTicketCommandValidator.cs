using FluentValidation;

namespace Application.Features.SupportTickets.Commands.Delete;

public class DeleteSupportTicketCommandValidator : AbstractValidator<DeleteSupportTicketCommand>
{
    public DeleteSupportTicketCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
