using FluentValidation;

namespace Application.Features.WorkoutTemplateDays.Commands.Create;

public class CreateWorkoutTemplateDayCommandValidator : AbstractValidator<CreateWorkoutTemplateDayCommand>
{
    public CreateWorkoutTemplateDayCommandValidator()
    {
        RuleFor(c => c.DayNumber).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Notes).NotEmpty();
        RuleFor(c => c.WorkoutTemplateId).NotEmpty();
    }
}
