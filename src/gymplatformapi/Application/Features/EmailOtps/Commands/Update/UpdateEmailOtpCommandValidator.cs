using FluentValidation;

namespace Application.Features.EmailOtps.Commands.Update;

public class UpdateEmailOtpCommandValidator : AbstractValidator<UpdateEmailOtpCommand>
{
    public UpdateEmailOtpCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.CodeHash).NotEmpty();
        RuleFor(c => c.Purpose).NotEmpty();
        RuleFor(c => c.ExpiresAt).NotEmpty();
        RuleFor(c => c.UsedDate).NotEmpty();
        RuleFor(c => c.IsUsed).NotEmpty();
        RuleFor(c => c.TryCount).NotEmpty();
        RuleFor(c => c.TenantId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}
