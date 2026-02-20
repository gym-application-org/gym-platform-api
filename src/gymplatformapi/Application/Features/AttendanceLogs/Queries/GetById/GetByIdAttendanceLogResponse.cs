using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.AttendanceLogs.Queries.GetById;

public class GetByIdAttendanceLogResponse : IResponse
{
    public int Id { get; set; }
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid MemberId { get; set; }
    public int GateId { get; set; }
}
