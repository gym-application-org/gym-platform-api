using FluentValidation;

namespace Application.Features.SupportTickets.Commands.UpdateAdmin;

public class UpdateAdminSupportTicketCommandValidator : AbstractValidator<UpdateAdminSupportTicketCommand>
{
    public UpdateAdminSupportTicketCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.Priority).NotEmpty();
        RuleFor(c => c.ClosedAt).NotEmpty();
    }
}
