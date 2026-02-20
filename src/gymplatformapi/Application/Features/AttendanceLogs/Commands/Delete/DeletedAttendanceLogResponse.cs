using Core.Application.Responses;

namespace Application.Features.AttendanceLogs.Commands.Delete;

public class DeletedAttendanceLogResponse : IResponse
{
    public int Id { get; set; }
}
