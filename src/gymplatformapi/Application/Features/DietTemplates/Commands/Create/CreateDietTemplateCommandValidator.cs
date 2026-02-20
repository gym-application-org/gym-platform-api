using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Create;

public class CreateDietTemplateCommandValidator : AbstractValidator<CreateDietTemplateCommand>
{
    public CreateDietTemplateCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.CaloriesTarget).NotEmpty();
        RuleFor(c => c.ProteinGramsTarget).NotEmpty();
        RuleFor(c => c.CarbGramsTarget).NotEmpty();
        RuleFor(c => c.FatGramsTarget).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
