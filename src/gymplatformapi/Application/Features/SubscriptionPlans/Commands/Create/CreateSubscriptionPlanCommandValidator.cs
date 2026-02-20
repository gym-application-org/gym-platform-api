using FluentValidation;

namespace Application.Features.SubscriptionPlans.Commands.Create;

public class CreateSubscriptionPlanCommandValidator : AbstractValidator<CreateSubscriptionPlanCommand>
{
    public CreateSubscriptionPlanCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.DurationDays).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
        RuleFor(c => c.Currency).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
