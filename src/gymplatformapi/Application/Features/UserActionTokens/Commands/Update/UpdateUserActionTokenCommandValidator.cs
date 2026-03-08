using FluentValidation;

namespace Application.Features.UserActionTokens.Commands.Update;

public class UpdateUserActionTokenCommandValidator : AbstractValidator<UpdateUserActionTokenCommand>
{
    public UpdateUserActionTokenCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.TenantId).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Purpose).NotEmpty();
        RuleFor(c => c.TargetType).NotEmpty();
        RuleFor(c => c.TargetEntityId).NotEmpty();
        RuleFor(c => c.TokenHash).NotEmpty();
        RuleFor(c => c.ExpiresAt).NotEmpty();
        RuleFor(c => c.UsedAt).NotEmpty();
        RuleFor(c => c.RevokedAt).NotEmpty();
        RuleFor(c => c.ReplacedByInvitationId).NotEmpty();
        RuleFor(c => c.CreatedByUserId).NotEmpty();
        RuleFor(c => c.MetadataJson).NotEmpty();
    }
}
