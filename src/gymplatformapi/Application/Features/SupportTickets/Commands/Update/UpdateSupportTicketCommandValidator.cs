using FluentValidation;

namespace Application.Features.SupportTickets.Commands.Update;

public class UpdateSupportTicketCommandValidator : AbstractValidator<UpdateSupportTicketCommand>
{
    public UpdateSupportTicketCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.Priority).NotEmpty();
        RuleFor(c => c.ClosedAt).NotEmpty();
        RuleFor(c => c.CreatedByStaffId).NotEmpty();
    }
}
