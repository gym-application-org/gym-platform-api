using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.UserForRegisterDto.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.UserForRegisterDto.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.UserForRegisterDto.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(c => c.UserForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100)
            .Must(StrongPassword)
            .WithMessage(
                "Your password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character."
            );
    }

    private bool StrongPassword(string arg)
    {
        Regex regex = new(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        return regex.IsMatch(arg);
    }
}
