using FluentValidation;

namespace Application.Features.WorkoutAssignments.Commands.CreateBulk;

public class CreateBulkWorkoutAssignmentCommandValidator : AbstractValidator<CreateBulkWorkoutAssignmentCommand>
{
    public CreateBulkWorkoutAssignmentCommandValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.MemberIds).NotEmpty();
        RuleFor(c => c.WorkoutTemplateId).NotEmpty();
    }
}
