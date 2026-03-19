using FluentValidation;

namespace Application.Features.Subscriptions.Commands.Create;

public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();

        RuleFor(c => c.EndDate).NotEmpty().GreaterThan(c => c.StartDate).WithMessage("End date must be after start date");

        RuleFor(c => c.Status).IsInEnum();

        RuleFor(c => c.PurchasedPlanName).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(c => c.PurchasedDurationDays).GreaterThan(0).LessThanOrEqualTo(3650);

        RuleFor(c => c.PurchasedUnitPrice).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1000000);

        RuleFor(c => c.Currency)
            .NotEmpty()
            .Length(3)
            .Matches(@"^[A-Z]{3}$")
            .WithMessage("Currency must be a 3-letter ISO code (e.g., USD, EUR, TRY)");

        RuleFor(c => c.DiscountAmount)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(c => c.PurchasedUnitPrice)
            .WithMessage("Discount cannot exceed the unit price");

        RuleFor(c => c.TotalPaid).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1000000);

        RuleFor(c => c.MemberId).NotEmpty();

        RuleFor(c => c.SubscriptionPlanId).GreaterThan(0);
    }
}
