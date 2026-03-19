using FluentValidation;

namespace Application.Features.Exercises.Commands.Delete;

public class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
{
    public DeleteExerciseCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
