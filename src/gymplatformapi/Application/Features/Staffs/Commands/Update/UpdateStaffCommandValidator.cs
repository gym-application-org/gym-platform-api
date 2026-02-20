using FluentValidation;

namespace Application.Features.Staffs.Commands.Update;

public class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
{
    public UpdateStaffCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Role).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}
