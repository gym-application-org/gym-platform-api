using FluentValidation;

namespace Application.Features.Members.Commands.Update;

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();

        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);

        RuleFor(c => c.Phone)
            .MinimumLength(10)
            .MaximumLength(20)
            .Matches(@"^[\d\s\+\-\(\)]+$")
            .When(c => !string.IsNullOrWhiteSpace(c.Phone))
            .WithMessage("Phone must contain only digits, spaces, +, -, (, )");

        RuleFor(c => c.Email).EmailAddress().MaximumLength(255).When(c => !string.IsNullOrWhiteSpace(c.Email));

        RuleFor(c => c.Status).IsInEnum();
    }
}
