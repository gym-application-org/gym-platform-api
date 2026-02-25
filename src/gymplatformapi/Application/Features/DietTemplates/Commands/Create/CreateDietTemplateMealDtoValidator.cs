using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.DietTemplates.Commands.Create.Dtos;
using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Create;

public class CreateDietTemplateMealDtoValidator : AbstractValidator<CreateDietTemplateMealDto>
{
    public CreateDietTemplateMealDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Order).GreaterThan(0);

        RuleForEach(x => x.Items).SetValidator(new CreateDietTemplateMealItemDtoValidator());
    }
}
