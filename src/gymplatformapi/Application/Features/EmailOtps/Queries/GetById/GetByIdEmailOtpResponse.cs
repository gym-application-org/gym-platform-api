using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.EmailOtps.Queries.GetById;

public class GetByIdEmailOtpResponse : IResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string CodeHash { get; set; }
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedDate { get; set; }
    public bool IsUsed { get; set; }
    public int TryCount { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? UserId { get; set; }
}
