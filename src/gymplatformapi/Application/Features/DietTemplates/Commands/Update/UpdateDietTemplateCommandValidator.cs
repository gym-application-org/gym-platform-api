using Application.Features.DietTemplates.Commands.Create;
using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Update;

public class UpdateDietTemplateCommandValidator : AbstractValidator<UpdateDietTemplateCommand>
{
    public UpdateDietTemplateCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);

        RuleFor(c => c.Description).MaximumLength(1000);

        RuleFor(c => c.CaloriesTarget).GreaterThan(0).When(c => c.CaloriesTarget.HasValue);

        RuleFor(c => c.ProteinGramsTarget).GreaterThanOrEqualTo(0).When(c => c.ProteinGramsTarget.HasValue);

        RuleFor(c => c.CarbGramsTarget).GreaterThanOrEqualTo(0).When(c => c.CarbGramsTarget.HasValue);

        RuleFor(c => c.FatGramsTarget).GreaterThanOrEqualTo(0).When(c => c.FatGramsTarget.HasValue);

        RuleForEach(c => c.Days).SetValidator(new UpdateDietTemplateDayDtoValidator());
    }
}
