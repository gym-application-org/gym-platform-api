using FluentValidation;

namespace Application.Features.EmailOtps.Commands.Update;

public class UpdateEmailOtpCommandValidator : AbstractValidator<UpdateEmailOtpCommand>
{
    public UpdateEmailOtpCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();

        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(c => c.CodeHash).NotEmpty().MinimumLength(10);

        RuleFor(c => c.Purpose).IsInEnum();

        RuleFor(c => c.ExpiresAt).NotEmpty();

        RuleFor(c => c.TryCount).InclusiveBetween(0, 10);

        RuleFor(c => c.TenantId).NotEmpty().When(c => c.TenantId.HasValue);

        RuleFor(c => c.UserId).NotEmpty().When(c => c.UserId.HasValue);
    }
}
