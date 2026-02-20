using FluentValidation;

namespace Application.Features.DietTemplateDays.Commands.Update;

public class UpdateDietTemplateDayCommandValidator : AbstractValidator<UpdateDietTemplateDayCommand>
{
    public UpdateDietTemplateDayCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.DayNumber).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Notes).NotEmpty();
        RuleFor(c => c.DietTemplateId).NotEmpty();
    }
}
