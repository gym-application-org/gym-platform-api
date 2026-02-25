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

public class UpdateDietTemplateMealDtoValidator : AbstractValidator<UpdateDietTemplateMealDto>
{
    public UpdateDietTemplateMealDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Order).GreaterThan(0);

        RuleForEach(x => x.Items).SetValidator(new UpdateDietTemplateMealItemDtoValidator());
    }
}
