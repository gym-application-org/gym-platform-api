using FluentValidation;

namespace Application.Features.Staffs.Commands.Delete;

public class DeleteStaffCommandValidator : AbstractValidator<DeleteStaffCommand>
{
    public DeleteStaffCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
