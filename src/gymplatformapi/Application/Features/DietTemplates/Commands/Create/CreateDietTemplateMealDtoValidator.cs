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
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(x => x.Order).GreaterThan(0).LessThanOrEqualTo(20);

        RuleFor(x => x.Notes).MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Notes));

        RuleForEach(x => x.Items).SetValidator(new CreateDietTemplateMealItemDtoValidator());
    }
}
