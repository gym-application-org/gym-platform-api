using FluentValidation;

namespace Application.Features.WorkoutAssignments.Commands.Create;

public class CreateWorkoutAssignmentCommandValidator : AbstractValidator<CreateWorkoutAssignmentCommand>
{
    public CreateWorkoutAssignmentCommandValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();

        RuleFor(c => c.EndDate)
            .GreaterThan(c => c.StartDate)
            .When(c => c.EndDate.HasValue)
            .WithMessage("End date must be after start date");

        RuleFor(c => c.Status).IsInEnum();

        RuleFor(c => c.MemberId).NotEmpty();

        RuleFor(c => c.WorkoutTemplateId).GreaterThan(0);
    }
}
