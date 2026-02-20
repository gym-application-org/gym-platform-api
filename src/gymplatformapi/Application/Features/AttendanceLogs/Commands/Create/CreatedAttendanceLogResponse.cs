using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.AttendanceLogs.Commands.Create;

public class CreatedAttendanceLogResponse : IResponse
{
    public int Id { get; set; }
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }
    public Guid MemberId { get; set; }
    public int GateId { get; set; }
}
