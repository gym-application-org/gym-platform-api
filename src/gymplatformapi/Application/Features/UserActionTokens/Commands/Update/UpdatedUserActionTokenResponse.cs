using Core.Application.Responses;
using Domain.Enums;
using Domain.Enums;

namespace Application.Features.UserActionTokens.Commands.Update;

public class UpdatedUserActionTokenResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? TenantId { get; set; }
    public string Email { get; set; }
    public UserActionPurpose Purpose { get; set; }
    public UserActionTargetType? TargetType { get; set; }
    public Guid? TargetEntityId { get; set; }
    public string TokenHash { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public Guid? ReplacedByInvitationId { get; set; }
    public Guid? CreatedByUserId { get; set; }
    public string? MetadataJson { get; set; }
}
