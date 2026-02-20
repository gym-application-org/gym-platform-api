using FluentValidation;

namespace Application.Features.DietTemplates.Commands.Update;

public class UpdateDietTemplateCommandValidator : AbstractValidator<UpdateDietTemplateCommand>
{
    public UpdateDietTemplateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.CaloriesTarget).NotEmpty();
        RuleFor(c => c.ProteinGramsTarget).NotEmpty();
        RuleFor(c => c.CarbGramsTarget).NotEmpty();
        RuleFor(c => c.FatGramsTarget).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
