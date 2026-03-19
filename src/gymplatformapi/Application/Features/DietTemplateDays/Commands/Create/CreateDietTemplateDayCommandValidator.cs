using FluentValidation;

namespace Application.Features.DietTemplateDays.Commands.Create;

public class CreateDietTemplateDayCommandValidator : AbstractValidator<CreateDietTemplateDayCommand>
{
    public CreateDietTemplateDayCommandValidator()
    {
        RuleFor(c => c.DayNumber).GreaterThan(0).LessThanOrEqualTo(365);

        RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Notes).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Notes));

        RuleFor(c => c.DietTemplateId).GreaterThan(0);
    }
}
