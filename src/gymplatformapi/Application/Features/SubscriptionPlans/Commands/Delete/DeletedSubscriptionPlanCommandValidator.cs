using FluentValidation;

namespace Application.Features.SubscriptionPlans.Commands.Delete;

public class DeleteSubscriptionPlanCommandValidator : AbstractValidator<DeleteSubscriptionPlanCommand>
{
    public DeleteSubscriptionPlanCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
