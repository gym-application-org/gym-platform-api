using FluentValidation;

namespace Application.Features.UserActionTokens.Commands.Delete;

public class DeleteUserActionTokenCommandValidator : AbstractValidator<DeleteUserActionTokenCommand>
{
    public DeleteUserActionTokenCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
