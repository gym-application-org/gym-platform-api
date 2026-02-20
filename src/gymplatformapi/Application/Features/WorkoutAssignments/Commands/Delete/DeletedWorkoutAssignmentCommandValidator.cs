using FluentValidation;

namespace Application.Features.WorkoutAssignments.Commands.Delete;

public class DeleteWorkoutAssignmentCommandValidator : AbstractValidator<DeleteWorkoutAssignmentCommand>
{
    public DeleteWorkoutAssignmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
