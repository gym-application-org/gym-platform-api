using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreateWorkoutTemplateCommandValidator : AbstractValidator<CreateWorkoutTemplateCommand>
{
    public CreateWorkoutTemplateCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(c => c.Description).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Description));

        RuleFor(c => c.Level).IsInEnum();

        RuleFor(c => c.Days).NotNull();

        RuleForEach(c => c.Days).SetValidator(new CreateWorkoutTemplateDayDtoValidator());
    }
}
