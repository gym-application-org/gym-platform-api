using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.AttendanceLogs.Queries.GetAdminList;

public class GetAdminListAttendanceLogListItemDto : IDto
{
    public int Id { get; set; }
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid MemberId { get; set; }
    public Guid TenantId { get; set; }
    public int GateId { get; set; }
}
