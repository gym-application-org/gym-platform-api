using FluentValidation;

namespace Application.Features.Gates.Commands.Update;

public class UpdateGateCommandValidator : AbstractValidator<UpdateGateCommand>
{
    public UpdateGateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.GateCode).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}
