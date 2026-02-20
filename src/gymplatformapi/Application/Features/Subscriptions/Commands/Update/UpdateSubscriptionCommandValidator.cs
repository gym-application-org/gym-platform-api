using FluentValidation;

namespace Application.Features.Subscriptions.Commands.Update;

public class UpdateSubscriptionCommandValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.PurchasedPlanName).NotEmpty();
        RuleFor(c => c.PurchasedDurationDays).NotEmpty();
        RuleFor(c => c.PurchasedUnitPrice).NotEmpty();
        RuleFor(c => c.Currency).NotEmpty();
        RuleFor(c => c.DiscountAmount).NotEmpty();
        RuleFor(c => c.TotalPaid).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.SubscriptionPlanId).NotEmpty();
    }
}
