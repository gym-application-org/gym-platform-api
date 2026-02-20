using FluentValidation;

namespace Application.Features.DietAssignments.Commands.Create;

public class CreateDietAssignmentCommandValidator : AbstractValidator<CreateDietAssignmentCommand>
{
    public CreateDietAssignmentCommandValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.Member).NotEmpty();
        RuleFor(c => c.DietTemplateId).NotEmpty();
    }
}
