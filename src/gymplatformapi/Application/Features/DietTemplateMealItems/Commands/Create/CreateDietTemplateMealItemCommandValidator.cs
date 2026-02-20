using FluentValidation;

namespace Application.Features.DietTemplateMealItems.Commands.Create;

public class CreateDietTemplateMealItemCommandValidator : AbstractValidator<CreateDietTemplateMealItemCommand>
{
    public CreateDietTemplateMealItemCommandValidator()
    {
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.FoodName).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
        RuleFor(c => c.Unit).NotEmpty();
        RuleFor(c => c.Calories).NotEmpty();
        RuleFor(c => c.ProteinG).NotEmpty();
        RuleFor(c => c.CarbG).NotEmpty();
        RuleFor(c => c.FatG).NotEmpty();
        RuleFor(c => c.Note).NotEmpty();
        RuleFor(c => c.DietTemplateMealId).NotEmpty();
    }
}
