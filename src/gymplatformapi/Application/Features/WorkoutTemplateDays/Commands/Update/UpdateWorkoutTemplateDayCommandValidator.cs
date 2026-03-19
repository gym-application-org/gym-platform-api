using FluentValidation;

namespace Application.Features.WorkoutTemplateDays.Commands.Update;

public class UpdateWorkoutTemplateDayCommandValidator : AbstractValidator<UpdateWorkoutTemplateDayCommand>
{
    public UpdateWorkoutTemplateDayCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.DayNumber).GreaterThan(0).LessThanOrEqualTo(365);

        RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Notes).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Notes));

        RuleFor(c => c.WorkoutTemplateId).GreaterThan(0);
    }
}
