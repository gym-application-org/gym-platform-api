using FluentValidation;

namespace Application.Features.SupportTickets.Commands.DeleteAdmin;

public class DeleteAdminSupportTicketCommandValidator : AbstractValidator<DeleteAdminSupportTicketCommand>
{
    public DeleteAdminSupportTicketCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
