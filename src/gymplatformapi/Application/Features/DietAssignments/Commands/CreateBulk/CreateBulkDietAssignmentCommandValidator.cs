using FluentValidation;

namespace Application.Features.DietAssignments.Commands.CreateBulk;

public class CreateBulkDietAssignmentCommandValidator : AbstractValidator<CreateBulkDietAssignmentCommand>
{
    public CreateBulkDietAssignmentCommandValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.MemberIds).NotEmpty();
        RuleFor(c => c.DietTemplateId).NotEmpty();
    }
}
