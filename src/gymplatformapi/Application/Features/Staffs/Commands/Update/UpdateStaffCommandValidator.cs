using FluentValidation;

namespace Application.Features.Staffs.Commands.Update;

public class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
{
    public UpdateStaffCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();

        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Phone)
            .MinimumLength(10)
            .MaximumLength(20)
            .Matches(@"^[\d\s\+\-\(\)]+$")
            .When(c => !string.IsNullOrWhiteSpace(c.Phone))
            .WithMessage("Phone must contain only digits, spaces, +, -, (, )");

        RuleFor(c => c.Email).EmailAddress().MaximumLength(255).When(c => !string.IsNullOrWhiteSpace(c.Email));
    }
}
