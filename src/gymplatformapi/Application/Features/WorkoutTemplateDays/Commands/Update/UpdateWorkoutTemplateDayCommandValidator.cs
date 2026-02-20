using FluentValidation;

namespace Application.Features.WorkoutTemplateDays.Commands.Update;

public class UpdateWorkoutTemplateDayCommandValidator : AbstractValidator<UpdateWorkoutTemplateDayCommand>
{
    public UpdateWorkoutTemplateDayCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.DayNumber).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Notes).NotEmpty();
        RuleFor(c => c.WorkoutTemplateId).NotEmpty();
    }
}
