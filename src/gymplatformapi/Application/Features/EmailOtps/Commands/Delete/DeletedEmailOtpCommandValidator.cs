using FluentValidation;

namespace Application.Features.EmailOtps.Commands.Delete;

public class DeleteEmailOtpCommandValidator : AbstractValidator<DeleteEmailOtpCommand>
{
    public DeleteEmailOtpCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
