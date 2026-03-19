using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.DietTemplates.Commands.Create.Dtos;
using Application.Features.DietTemplates.Commands.Update.Dtos;
using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Update;

public class UpdateDietTemplateMealItemDtoValidator : AbstractValidator<UpdateDietTemplateMealItemDto>
{
    public UpdateDietTemplateMealItemDtoValidator()
    {
        RuleFor(x => x.Order).GreaterThan(0).LessThanOrEqualTo(50);

        RuleFor(x => x.FoodName).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(10000).When(x => x.Quantity.HasValue);

        RuleFor(x => x.Unit).MinimumLength(1).MaximumLength(20).When(x => !string.IsNullOrWhiteSpace(x.Unit));

        RuleFor(x => x.Calories).InclusiveBetween(0, 5000).When(x => x.Calories.HasValue);

        RuleFor(x => x.ProteinG).InclusiveBetween(0, 500).When(x => x.ProteinG.HasValue);

        RuleFor(x => x.CarbG).InclusiveBetween(0, 1000).When(x => x.CarbG.HasValue);

        RuleFor(x => x.FatG).InclusiveBetween(0, 300).When(x => x.FatG.HasValue);

        RuleFor(x => x.Note).MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Note));
    }
}
