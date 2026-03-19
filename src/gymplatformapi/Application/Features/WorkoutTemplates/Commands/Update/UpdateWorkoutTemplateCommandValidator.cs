using Application.Features.WorkoutTemplates.Commands.Create;
using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Update;

public class UpdateWorkoutTemplateCommandValidator : AbstractValidator<UpdateWorkoutTemplateCommand>
{
    public UpdateWorkoutTemplateCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(c => c.Description).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Description));

        RuleFor(c => c.Level).IsInEnum();

        RuleFor(c => c.Days).NotNull();

        RuleForEach(c => c.Days).SetValidator(new UpdateWorkoutTemplateDayDtoValidator());
    }
}
