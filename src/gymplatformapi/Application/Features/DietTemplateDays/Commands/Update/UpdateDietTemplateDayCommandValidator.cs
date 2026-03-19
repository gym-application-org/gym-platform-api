using FluentValidation;

namespace Application.Features.DietTemplateDays.Commands.Update;

public class UpdateDietTemplateDayCommandValidator : AbstractValidator<UpdateDietTemplateDayCommand>
{
    public UpdateDietTemplateDayCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.DayNumber).GreaterThan(0).LessThanOrEqualTo(365);

        RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Notes).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Notes));

        RuleFor(c => c.DietTemplateId).GreaterThan(0);
    }
}
