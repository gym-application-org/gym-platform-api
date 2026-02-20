using FluentValidation;

namespace Application.Features.WorkoutTemplateDayExercises.Commands.Create;

public class CreateWorkoutTemplateDayExerciseCommandValidator : AbstractValidator<CreateWorkoutTemplateDayExerciseCommand>
{
    public CreateWorkoutTemplateDayExerciseCommandValidator()
    {
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.Sets).NotEmpty();
        RuleFor(c => c.Reps).NotEmpty();
        RuleFor(c => c.WeightKg).NotEmpty();
        RuleFor(c => c.RestSeconds).NotEmpty();
        RuleFor(c => c.Tempo).NotEmpty();
        RuleFor(c => c.Note).NotEmpty();
        RuleFor(c => c.WorkoutTemplateDayId).NotEmpty();
        RuleFor(c => c.ExerciseId).NotEmpty();
    }
}
