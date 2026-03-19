using FluentValidation;

namespace Application.Features.Exercises.Commands.Update;

public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(120);

        RuleFor(c => c.Description).MaximumLength(2000).When(c => !string.IsNullOrWhiteSpace(c.Description));

        RuleFor(c => c.MuscleGroup).MaximumLength(80).When(c => !string.IsNullOrWhiteSpace(c.MuscleGroup));

        RuleFor(c => c.Equipment).MaximumLength(80).When(c => !string.IsNullOrWhiteSpace(c.Equipment));

        RuleFor(c => c.DifficultyLevel).IsInEnum();

        RuleFor(c => c.VideoUrl)
            .Must(BeValidAbsoluteUrl)
            .When(c => !string.IsNullOrWhiteSpace(c.VideoUrl))
            .WithMessage("VideoUrl must be a valid absolute URL");

        RuleFor(c => c.ThumbnailUrl)
            .Must(BeValidAbsoluteUrl)
            .When(c => !string.IsNullOrWhiteSpace(c.ThumbnailUrl))
            .WithMessage("ThumbnailUrl must be a valid absolute URL");
    }

    private static bool BeValidAbsoluteUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
