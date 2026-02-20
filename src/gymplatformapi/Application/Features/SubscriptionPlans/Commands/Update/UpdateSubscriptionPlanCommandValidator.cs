using FluentValidation;

namespace Application.Features.SubscriptionPlans.Commands.Update;

public class UpdateSubscriptionPlanCommandValidator : AbstractValidator<UpdateSubscriptionPlanCommand>
{
    public UpdateSubscriptionPlanCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.DurationDays).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
        RuleFor(c => c.Currency).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
