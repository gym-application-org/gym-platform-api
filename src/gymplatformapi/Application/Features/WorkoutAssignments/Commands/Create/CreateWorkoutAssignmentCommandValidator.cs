using FluentValidation;

namespace Application.Features.WorkoutAssignments.Commands.Create;

public class CreateWorkoutAssignmentCommandValidator : AbstractValidator<CreateWorkoutAssignmentCommand>
{
    public CreateWorkoutAssignmentCommandValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.WorkoutTemplateId).NotEmpty();
    }
}
