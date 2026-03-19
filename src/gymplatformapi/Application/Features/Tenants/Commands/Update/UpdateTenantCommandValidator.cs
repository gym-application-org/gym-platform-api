using FluentValidation;

namespace Application.Features.Tenants.Commands.Update;

public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();

        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(200);
    }
}
