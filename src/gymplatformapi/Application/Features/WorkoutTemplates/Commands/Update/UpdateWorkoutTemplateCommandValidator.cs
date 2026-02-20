using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Update;

public class UpdateWorkoutTemplateCommandValidator : AbstractValidator<UpdateWorkoutTemplateCommand>
{
    public UpdateWorkoutTemplateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Level).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
