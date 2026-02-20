using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreateWorkoutTemplateCommandValidator : AbstractValidator<CreateWorkoutTemplateCommand>
{
    public CreateWorkoutTemplateCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Level).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
