using Application.Features.DietTemplates.Commands.Create;
using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Update;

public class UpdateDietTemplateCommandValidator : AbstractValidator<UpdateDietTemplateCommand>
{
    public UpdateDietTemplateCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(c => c.Description).MaximumLength(1000).When(c => !string.IsNullOrWhiteSpace(c.Description));

        RuleFor(c => c.CaloriesTarget)
            .InclusiveBetween(500, 10000)
            .When(c => c.CaloriesTarget.HasValue)
            .WithMessage("Calories target must be between 500 and 10000");

        RuleFor(c => c.ProteinGramsTarget)
            .InclusiveBetween(0, 1000)
            .When(c => c.ProteinGramsTarget.HasValue)
            .WithMessage("Protein target must be between 0 and 1000 grams");

        RuleFor(c => c.CarbGramsTarget)
            .InclusiveBetween(0, 2000)
            .When(c => c.CarbGramsTarget.HasValue)
            .WithMessage("Carbs target must be between 0 and 2000 grams");

        RuleFor(c => c.FatGramsTarget)
            .InclusiveBetween(0, 500)
            .When(c => c.FatGramsTarget.HasValue)
            .WithMessage("Fat target must be between 0 and 500 grams");

        RuleForEach(c => c.Days).SetValidator(new UpdateDietTemplateDayDtoValidator());
    }
}
