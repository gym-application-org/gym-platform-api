using FluentValidation;

namespace Application.Features.Exercises.Commands.Create;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(120);

        RuleFor(c => c.Description).MaximumLength(2000);

        RuleFor(c => c.MuscleGroup).MaximumLength(80);

        RuleFor(c => c.Equipment).MaximumLength(80);

        RuleFor(c => c.DifficultyLevel).IsInEnum();

        RuleFor(c => c.VideoUrl).NotNull().NotEmpty().Must(BeValidAbsoluteUrl).WithMessage("VideoUrl must be a valid absolute URL.");

        RuleFor(c => c.ThumbnailUrl)
            .NotNull()
            .NotEmpty()
            .Must(BeValidAbsoluteUrl)
            .WithMessage("ThumbnailUrl must be a valid absolute URL.");
    }

    private static bool BeValidAbsoluteUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
