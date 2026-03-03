using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreateWorkoutTemplateCommandValidator : AbstractValidator<CreateWorkoutTemplateCommand>
{
    public CreateWorkoutTemplateCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);

        RuleFor(c => c.Description).MaximumLength(1000);

        RuleFor(c => c.Level).IsInEnum();

        RuleFor(c => c.Days).NotNull();

        RuleForEach(c => c.Days).SetValidator(new CreateWorkoutTemplateDayDtoValidator());
    }
}
