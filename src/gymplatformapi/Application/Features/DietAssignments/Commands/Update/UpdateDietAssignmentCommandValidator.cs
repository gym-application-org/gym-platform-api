using FluentValidation;

namespace Application.Features.DietAssignments.Commands.Update;

public class UpdateDietAssignmentCommandValidator : AbstractValidator<UpdateDietAssignmentCommand>
{
    public UpdateDietAssignmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.Member).NotEmpty();
        RuleFor(c => c.DietTemplateId).NotEmpty();
    }
}
