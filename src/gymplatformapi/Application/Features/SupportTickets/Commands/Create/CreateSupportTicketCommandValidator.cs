using FluentValidation;

namespace Application.Features.SupportTickets.Commands.Create;

public class CreateSupportTicketCommandValidator : AbstractValidator<CreateSupportTicketCommand>
{
    public CreateSupportTicketCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty().MinimumLength(3).MaximumLength(200);

        RuleFor(c => c.Description).NotEmpty().MinimumLength(10).MaximumLength(5000);

        RuleFor(c => c.Status).IsInEnum();

        RuleFor(c => c.Priority).IsInEnum();

        RuleFor(c => c.ClosedAt)
            .GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5))
            .When(c => c.ClosedAt.HasValue)
            .WithMessage("Closed date cannot be in the past");
    }
}
