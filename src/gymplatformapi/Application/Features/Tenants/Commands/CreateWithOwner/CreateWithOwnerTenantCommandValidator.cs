using FluentValidation;

namespace Application.Features.Tenants.Commands.Create;

public class CreateWithOwnerTenantCommandValidator : AbstractValidator<CreateWithOwnerTenantCommand>
{
    public CreateWithOwnerTenantCommandValidator()
    {
        RuleFor(c => c.TenantName).NotEmpty().MinimumLength(2).MaximumLength(200);

        RuleFor(c => c.TenantSubdomain)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .Matches(@"^[a-z0-9\-]+$")
            .WithMessage("Subdomain must contain only lowercase letters, numbers, and hyphens");

        RuleFor(c => c.OwnerName).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(c => c.Phone)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(20)
            .Matches(@"^[\d\s\+\-\(\)]+$")
            .WithMessage("Phone must contain only digits, spaces, +, -, (, )");
    }
}
