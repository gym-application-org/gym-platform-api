using FluentValidation;

namespace Application.Features.DietTemplateMeals.Commands.Update;

public class UpdateDietTemplateMealCommandValidator : AbstractValidator<UpdateDietTemplateMealCommand>
{
    public UpdateDietTemplateMealCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Order).GreaterThan(0).LessThanOrEqualTo(20);

        RuleFor(c => c.Notes).MaximumLength(500).When(c => !string.IsNullOrWhiteSpace(c.Notes));

        RuleFor(c => c.DietTemplateDayId).GreaterThan(0);
    }
}
