using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.UserForRegisterDto.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.UserForRegisterDto.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.UserForRegisterDto.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(c => c.UserForRegisterDto.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
    }
}
