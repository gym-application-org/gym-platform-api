using FluentValidation;

namespace Application.Features.WorkoutTemplateDayExercises.Commands.Delete;

public class DeleteWorkoutTemplateDayExerciseCommandValidator : AbstractValidator<DeleteWorkoutTemplateDayExerciseCommand>
{
    public DeleteWorkoutTemplateDayExerciseCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
