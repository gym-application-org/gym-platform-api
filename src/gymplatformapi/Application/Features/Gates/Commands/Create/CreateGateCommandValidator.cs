using FluentValidation;

namespace Application.Features.Gates.Commands.Create;

public class CreateGateCommandValidator : AbstractValidator<CreateGateCommand>
{
    public CreateGateCommandValidator()
    {
        RuleFor(c => c.TenantId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.GateCode).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
