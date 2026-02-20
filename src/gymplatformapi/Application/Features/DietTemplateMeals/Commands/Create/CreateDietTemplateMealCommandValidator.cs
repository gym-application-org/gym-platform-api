using FluentValidation;

namespace Application.Features.DietTemplateMeals.Commands.Create;

public class CreateDietTemplateMealCommandValidator : AbstractValidator<CreateDietTemplateMealCommand>
{
    public CreateDietTemplateMealCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.Notes).NotEmpty();
        RuleFor(c => c.DietTemplateDayId).NotEmpty();
    }
}
