using FluentValidation;

namespace Application.Features.WorkoutTemplateDays.Commands.Create;

public class CreateWorkoutTemplateDayCommandValidator : AbstractValidator<CreateWorkoutTemplateDayCommand>
{
    public CreateWorkoutTemplateDayCommandValidator()
    {
        RuleFor(c => c.DayNumber).GreaterThan(0).LessThanOrEqualTo(365);

        RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Notes).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Notes));

        RuleFor(c => c.WorkoutTemplateId).GreaterThan(0);
    }
}
