using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkoutTemplates.Commands.Create;
using Application.Features.WorkoutTemplates.Commands.Create.Dtos;
using Application.Features.WorkoutTemplates.Commands.Update.Dtos;
using FluentValidation;

namespace Application.Features.WorkoutTemplates.Commands.Update;

public class UpdateWorkoutTemplateDayDtoValidator : AbstractValidator<UpdateWorkoutTemplateDayDto>
{
    public UpdateWorkoutTemplateDayDtoValidator()
    {
        RuleFor(x => x.DayNumber).GreaterThan(0);

        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);

        RuleForEach(x => x.Exercises).SetValidator(new UpdateWorkoutTemplateDayExerciseDtoValidator());
    }
}
