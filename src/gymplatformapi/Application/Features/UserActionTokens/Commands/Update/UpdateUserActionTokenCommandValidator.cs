using FluentValidation;

namespace Application.Features.UserActionTokens.Commands.Update;

public class UpdateUserActionTokenCommandValidator : AbstractValidator<UpdateUserActionTokenCommand>
{
    public UpdateUserActionTokenCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();

        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.TenantId).NotEmpty().When(c => c.TenantId.HasValue);

        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(c => c.Purpose).IsInEnum();

        RuleFor(c => c.TargetType).IsInEnum().When(c => c.TargetType.HasValue);

        RuleFor(c => c.TargetEntityId).NotEmpty().When(c => c.TargetEntityId.HasValue);

        RuleFor(c => c.TokenHash).NotEmpty().MinimumLength(10);

        RuleFor(c => c.ExpiresAt).NotEmpty();

        RuleFor(c => c.MetadataJson).MaximumLength(5000).When(c => !string.IsNullOrWhiteSpace(c.MetadataJson));
    }
}
