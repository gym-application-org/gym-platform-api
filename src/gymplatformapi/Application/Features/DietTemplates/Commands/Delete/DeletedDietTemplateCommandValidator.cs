using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Delete;

public class DeleteDietTemplateCommandValidator : AbstractValidator<DeleteDietTemplateCommand>
{
    public DeleteDietTemplateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
