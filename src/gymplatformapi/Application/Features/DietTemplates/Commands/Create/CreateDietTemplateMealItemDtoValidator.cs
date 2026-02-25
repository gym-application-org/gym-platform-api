using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.DietTemplates.Commands.Create.Dtos;
using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Create;

public class CreateDietTemplateMealItemDtoValidator : AbstractValidator<CreateDietTemplateMealItemDto>
{
    public CreateDietTemplateMealItemDtoValidator()
    {
        RuleFor(x => x.Order).GreaterThan(0);
        RuleFor(x => x.FoodName).NotEmpty().MaximumLength(200);

        RuleFor(x => x.Quantity).GreaterThan(0).When(x => x.Quantity.HasValue);

        RuleFor(x => x.Calories).GreaterThanOrEqualTo(0).When(x => x.Calories.HasValue);

        RuleFor(x => x.ProteinG).GreaterThanOrEqualTo(0).When(x => x.ProteinG.HasValue);

        RuleFor(x => x.CarbG).GreaterThanOrEqualTo(0).When(x => x.CarbG.HasValue);

        RuleFor(x => x.FatG).GreaterThanOrEqualTo(0).When(x => x.FatG.HasValue);
    }
}
