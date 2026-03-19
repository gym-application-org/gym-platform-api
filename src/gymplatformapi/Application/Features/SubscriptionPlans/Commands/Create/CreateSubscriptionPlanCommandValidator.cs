using FluentValidation;

namespace Application.Features.SubscriptionPlans.Commands.Create;

public class CreateSubscriptionPlanCommandValidator : AbstractValidator<CreateSubscriptionPlanCommand>
{
    public CreateSubscriptionPlanCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(c => c.DurationDays).GreaterThan(0).LessThanOrEqualTo(3650);

        RuleFor(c => c.Price).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1000000);

        RuleFor(c => c.Currency)
            .NotEmpty()
            .Length(3)
            .Matches(@"^[A-Z]{3}$")
            .WithMessage("Currency must be a 3-letter ISO code (e.g., USD, EUR, TRY)");

        RuleFor(c => c.Description).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Description));
    }
}
