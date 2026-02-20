using FluentValidation;

namespace Application.Features.WorkoutAssignments.Commands.Update;

public class UpdateWorkoutAssignmentCommandValidator : AbstractValidator<UpdateWorkoutAssignmentCommand>
{
    public UpdateWorkoutAssignmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.WorkoutTemplateId).NotEmpty();
    }
}
