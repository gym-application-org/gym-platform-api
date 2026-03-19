using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.DietTemplates.Commands.Create;
using Application.Features.DietTemplates.Commands.Create.Dtos;
using Application.Features.DietTemplates.Commands.Update.Dtos;
using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Update;

public class UpdateDietTemplateDayDtoValidator : AbstractValidator<UpdateDietTemplateDayDto>
{
    public UpdateDietTemplateDayDtoValidator()
    {
        RuleFor(x => x.DayNumber).GreaterThan(0).LessThanOrEqualTo(365);

        RuleFor(x => x.Title).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleForEach(x => x.Meals).SetValidator(new UpdateDietTemplateMealDtoValidator());
    }
}
