using FluentValidation;

namespace Application.Features.DietAssignments.Commands.Delete;

public class DeleteDietAssignmentCommandValidator : AbstractValidator<DeleteDietAssignmentCommand>
{
    public DeleteDietAssignmentCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
