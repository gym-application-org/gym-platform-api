using FluentValidation;

namespace Application.Features.SupportTickets.Commands.Create;

public class CreateSupportTicketCommandValidator : AbstractValidator<CreateSupportTicketCommand>
{
    public CreateSupportTicketCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.Priority).NotEmpty();
        RuleFor(c => c.ClosedAt).NotEmpty();
    }
}
