using FluentValidation;

namespace Application.Features.WorkoutAssignments.Commands.CreateBulk;

public class CreateBulkWorkoutAssignmentCommandValidator : AbstractValidator<CreateBulkWorkoutAssignmentCommand>
{
    public CreateBulkWorkoutAssignmentCommandValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();

        RuleFor(c => c.EndDate)
            .GreaterThan(c => c.StartDate)
            .When(c => c.EndDate.HasValue)
            .WithMessage("End date must be after start date");

        RuleFor(c => c.Status).IsInEnum();

        RuleFor(c => c.MemberIds)
            .NotEmpty()
            .Must(ids => ids.Count > 0 && ids.Count <= 1000)
            .WithMessage("Must assign to at least 1 and at most 1000 members");

        RuleFor(c => c.WorkoutTemplateId).GreaterThan(0);
    }
}
