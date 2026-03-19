using FluentValidation;

namespace Application.Features.WorkoutTemplateDayExercises.Commands.Update;

public class UpdateWorkoutTemplateDayExerciseCommandValidator : AbstractValidator<UpdateWorkoutTemplateDayExerciseCommand>
{
    public UpdateWorkoutTemplateDayExerciseCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.Order).GreaterThan(0).LessThanOrEqualTo(50);

        RuleFor(c => c.Sets).GreaterThan(0).LessThanOrEqualTo(20);

        RuleFor(c => c.Reps).NotEmpty().MaximumLength(50);

        RuleFor(c => c.WeightKg).GreaterThan(0).LessThanOrEqualTo(1000).When(c => c.WeightKg.HasValue);

        RuleFor(c => c.RestSeconds).InclusiveBetween(0, 600).When(c => c.RestSeconds.HasValue);

        RuleFor(c => c.Tempo).MaximumLength(20).When(c => !string.IsNullOrWhiteSpace(c.Tempo));

        RuleFor(c => c.Note).MaximumLength(500).When(c => !string.IsNullOrWhiteSpace(c.Note));

        RuleFor(c => c.WorkoutTemplateDayId).GreaterThan(0);

        RuleFor(c => c.ExerciseId).GreaterThan(0).When(c => c.ExerciseId.HasValue);
    }
}
