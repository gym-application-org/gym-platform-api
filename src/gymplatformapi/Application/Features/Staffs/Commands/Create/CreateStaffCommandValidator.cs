using FluentValidation;

namespace Application.Features.Staffs.Commands.Create;

public class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
{
    public CreateStaffCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Role).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}
