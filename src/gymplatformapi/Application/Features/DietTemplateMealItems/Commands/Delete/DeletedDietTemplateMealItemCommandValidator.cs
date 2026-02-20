using FluentValidation;

namespace Application.Features.DietTemplateMealItems.Commands.Delete;

public class DeleteDietTemplateMealItemCommandValidator : AbstractValidator<DeleteDietTemplateMealItemCommand>
{
    public DeleteDietTemplateMealItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
