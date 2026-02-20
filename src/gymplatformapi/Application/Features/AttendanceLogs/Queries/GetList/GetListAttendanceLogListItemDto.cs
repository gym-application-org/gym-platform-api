using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.AttendanceLogs.Queries.GetList;

public class GetListAttendanceLogListItemDto : IDto
{
    public int Id { get; set; }
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }
    public Guid MemberId { get; set; }
    public int GateId { get; set; }
}
