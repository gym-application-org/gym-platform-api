using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkoutTemplates.Commands.Create.Dtos;
using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreateWorkoutTemplateDayDtoValidator : AbstractValidator<CreateWorkoutTemplateDayDto>
{
    public CreateWorkoutTemplateDayDtoValidator()
    {
        RuleFor(x => x.DayNumber).GreaterThan(0);

        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);

        RuleForEach(x => x.Exercises).SetValidator(new CreateWorkoutTemplateDayExerciseDtoValidator());
    }
}
