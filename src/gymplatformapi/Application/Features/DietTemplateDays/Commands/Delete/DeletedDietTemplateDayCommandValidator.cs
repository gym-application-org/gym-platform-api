using FluentValidation;

namespace Application.Features.DietTemplateDays.Commands.Delete;

public class DeleteDietTemplateDayCommandValidator : AbstractValidator<DeleteDietTemplateDayCommand>
{
    public DeleteDietTemplateDayCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
