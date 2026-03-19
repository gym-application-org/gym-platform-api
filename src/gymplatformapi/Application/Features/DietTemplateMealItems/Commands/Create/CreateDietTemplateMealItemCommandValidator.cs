using FluentValidation;

namespace Application.Features.DietTemplateMealItems.Commands.Create;

public class CreateDietTemplateMealItemCommandValidator : AbstractValidator<CreateDietTemplateMealItemCommand>
{
    public CreateDietTemplateMealItemCommandValidator()
    {
        RuleFor(c => c.Order).GreaterThan(0).LessThanOrEqualTo(50);

        RuleFor(c => c.FoodName).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(c => c.Quantity).GreaterThan(0).LessThanOrEqualTo(10000).When(c => c.Quantity.HasValue);

        RuleFor(c => c.Unit).MinimumLength(1).MaximumLength(20).When(c => !string.IsNullOrWhiteSpace(c.Unit));

        RuleFor(c => c.Calories).InclusiveBetween(0, 5000).When(c => c.Calories.HasValue);

        RuleFor(c => c.ProteinG).InclusiveBetween(0, 500).When(c => c.ProteinG.HasValue);

        RuleFor(c => c.CarbG).InclusiveBetween(0, 1000).When(c => c.CarbG.HasValue);

        RuleFor(c => c.FatG).InclusiveBetween(0, 300).When(c => c.FatG.HasValue);

        RuleFor(c => c.Note).MaximumLength(500).When(c => !string.IsNullOrWhiteSpace(c.Note));

        RuleFor(c => c.DietTemplateMealId).GreaterThan(0);
    }
}
