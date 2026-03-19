using FluentValidation;

namespace Application.Features.Gates.Commands.Create;

public class CreateGateCommandValidator : AbstractValidator<CreateGateCommand>
{
    public CreateGateCommandValidator()
    {
        RuleFor(c => c.TenantId).NotEmpty();

        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.GateCode)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .Matches(@"^[A-Za-z0-9\-_]+$")
            .WithMessage("Gate code must contain only letters, numbers, hyphens, and underscores");
    }
}
