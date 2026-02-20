using FluentValidation;

namespace Application.Features.DietTemplateDays.Commands.Create;

public class CreateDietTemplateDayCommandValidator : AbstractValidator<CreateDietTemplateDayCommand>
{
    public CreateDietTemplateDayCommandValidator()
    {
        RuleFor(c => c.DayNumber).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Notes).NotEmpty();
        RuleFor(c => c.DietTemplateId).NotEmpty();
    }
}
