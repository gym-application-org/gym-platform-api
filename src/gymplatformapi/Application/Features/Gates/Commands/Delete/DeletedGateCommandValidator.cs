using FluentValidation;

namespace Application.Features.Gates.Commands.Delete;

public class DeleteGateCommandValidator : AbstractValidator<DeleteGateCommand>
{
    public DeleteGateCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
