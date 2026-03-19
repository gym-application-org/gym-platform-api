using FluentValidation;

namespace Application.Features.DietTemplateMeals.Commands.Create;

public class CreateDietTemplateMealCommandValidator : AbstractValidator<CreateDietTemplateMealCommand>
{
    public CreateDietTemplateMealCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Order).GreaterThan(0).LessThanOrEqualTo(20);

        RuleFor(c => c.Notes).MaximumLength(500).When(c => !string.IsNullOrWhiteSpace(c.Notes));

        RuleFor(c => c.DietTemplateDayId).GreaterThan(0);
    }
}
