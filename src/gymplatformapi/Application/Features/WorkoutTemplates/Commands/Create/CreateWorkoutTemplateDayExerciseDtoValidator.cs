using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkoutTemplates.Commands.Create.Dtos;
using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreateWorkoutTemplateDayExerciseDtoValidator : AbstractValidator<WorkoutTemplateDayExerciseDto>
{
    public CreateWorkoutTemplateDayExerciseDtoValidator()
    {
        RuleFor(x => x.Order).GreaterThan(0).LessThanOrEqualTo(50);

        RuleFor(x => x.Sets).GreaterThan(0).LessThanOrEqualTo(20);

        RuleFor(x => x.Reps).NotEmpty().MaximumLength(50);

        RuleFor(x => x.WeightKg).GreaterThan(0).LessThanOrEqualTo(1000).When(x => x.WeightKg.HasValue);

        RuleFor(x => x.RestSeconds).InclusiveBetween(0, 600).When(x => x.RestSeconds.HasValue);

        RuleFor(x => x.Tempo).MaximumLength(20).When(x => !string.IsNullOrWhiteSpace(x.Tempo));

        RuleFor(x => x.Note).MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Note));

        RuleFor(x => x.ExerciseId).GreaterThan(0).When(x => x.ExerciseId.HasValue);
    }
}
