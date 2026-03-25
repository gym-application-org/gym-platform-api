using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.ActivateInvitedUser;

public class ActivateInvitedUserCommandValidator : AbstractValidator<ActivateInvitedUserCommand>
{
    public ActivateInvitedUserCommandValidator()
    {
        RuleFor(c => c.Token).NotEmpty();

        RuleFor(c => c.NewPassword)
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
