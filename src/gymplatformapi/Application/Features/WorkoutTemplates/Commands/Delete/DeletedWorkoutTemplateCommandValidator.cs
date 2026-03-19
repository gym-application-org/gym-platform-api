using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Delete;

public class DeleteWorkoutTemplateCommandValidator : AbstractValidator<DeleteWorkoutTemplateCommand>
{
    public DeleteWorkoutTemplateCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
