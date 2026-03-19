using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(c => c.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
    }
}
