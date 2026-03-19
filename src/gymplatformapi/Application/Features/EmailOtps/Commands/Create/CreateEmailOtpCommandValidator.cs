using FluentValidation;

namespace Application.Features.EmailOtps.Commands.Create;

public class CreateEmailOtpCommandValidator : AbstractValidator<CreateEmailOtpCommand>
{
    public CreateEmailOtpCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(c => c.CodeHash).NotEmpty().MinimumLength(10);

        RuleFor(c => c.Purpose).IsInEnum();

        RuleFor(c => c.ExpiresAt).NotEmpty().GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future");

        RuleFor(c => c.TryCount).InclusiveBetween(0, 10);

        RuleFor(c => c.TenantId).NotEmpty().When(c => c.TenantId.HasValue);

        RuleFor(c => c.UserId).NotEmpty().When(c => c.UserId.HasValue);
    }
}
