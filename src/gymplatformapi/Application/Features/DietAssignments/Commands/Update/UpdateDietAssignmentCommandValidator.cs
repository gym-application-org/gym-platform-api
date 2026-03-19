using FluentValidation;

namespace Application.Features.DietAssignments.Commands.Update;

public class UpdateDietAssignmentCommandValidator : AbstractValidator<UpdateDietAssignmentCommand>
{
    public UpdateDietAssignmentCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);

        RuleFor(c => c.StartDate).NotEmpty();

        RuleFor(c => c.EndDate)
            .GreaterThan(c => c.StartDate)
            .When(c => c.EndDate.HasValue)
            .WithMessage("End date must be after start date");

        RuleFor(c => c.Status).IsInEnum();

        RuleFor(c => c.MemberId).NotEmpty();

        RuleFor(c => c.DietTemplateId).GreaterThan(0);
    }
}
