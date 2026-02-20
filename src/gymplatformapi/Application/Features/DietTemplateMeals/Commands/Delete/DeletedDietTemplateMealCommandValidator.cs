using FluentValidation;

namespace Application.Features.DietTemplateMeals.Commands.Delete;

public class DeleteDietTemplateMealCommandValidator : AbstractValidator<DeleteDietTemplateMealCommand>
{
    public DeleteDietTemplateMealCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
