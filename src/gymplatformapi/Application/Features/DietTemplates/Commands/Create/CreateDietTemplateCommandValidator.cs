using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Create;

public class CreateDietTemplateCommandValidator : AbstractValidator<CreateDietTemplateCommand>
{
    public CreateDietTemplateCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);

        RuleFor(c => c.Description).MaximumLength(1000);

        RuleFor(c => c.CaloriesTarget).GreaterThan(0).When(c => c.CaloriesTarget.HasValue);

        RuleFor(c => c.ProteinGramsTarget).GreaterThanOrEqualTo(0).When(c => c.ProteinGramsTarget.HasValue);

        RuleFor(c => c.CarbGramsTarget).GreaterThanOrEqualTo(0).When(c => c.CarbGramsTarget.HasValue);

        RuleFor(c => c.FatGramsTarget).GreaterThanOrEqualTo(0).When(c => c.FatGramsTarget.HasValue);

        RuleFor(c => c.Days).NotNull();

        RuleForEach(c => c.Days).SetValidator(new CreateDietTemplateDayDtoValidator());
    }
}
