using FluentValidation;

namespace Application.Features.WorkoutTemplateDays.Commands.Delete;

public class DeleteWorkoutTemplateDayCommandValidator : AbstractValidator<DeleteWorkoutTemplateDayCommand>
{
    public DeleteWorkoutTemplateDayCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
