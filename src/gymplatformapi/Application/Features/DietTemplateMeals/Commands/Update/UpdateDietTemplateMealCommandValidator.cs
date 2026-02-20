using FluentValidation;

namespace Application.Features.DietTemplateMeals.Commands.Update;

public class UpdateDietTemplateMealCommandValidator : AbstractValidator<UpdateDietTemplateMealCommand>
{
    public UpdateDietTemplateMealCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.Notes).NotEmpty();
        RuleFor(c => c.DietTemplateDayId).NotEmpty();
    }
}
