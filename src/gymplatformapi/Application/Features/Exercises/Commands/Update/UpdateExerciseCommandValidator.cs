using FluentValidation;

namespace Application.Features.Exercises.Commands.Update;

public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.MuscleGroup).NotEmpty();
        RuleFor(c => c.Equipment).NotEmpty();
        RuleFor(c => c.DifficultyLevel).NotEmpty();
        RuleFor(c => c.VideoUrl).NotEmpty();
        RuleFor(c => c.ThumbnailUrl).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
