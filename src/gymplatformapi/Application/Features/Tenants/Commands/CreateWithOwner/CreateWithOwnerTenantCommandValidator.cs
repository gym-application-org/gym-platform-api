using FluentValidation;

namespace Application.Features.Tenants.Commands.Create;

public class CreateWithOwnerTenantCommandValidator : AbstractValidator<CreateWithOwnerTenantCommand>
{
    public CreateWithOwnerTenantCommandValidator()
    {
        RuleFor(c => c.TenantName).NotEmpty();
        RuleFor(c => c.TenantIsActive).NotEmpty();
    }
}
