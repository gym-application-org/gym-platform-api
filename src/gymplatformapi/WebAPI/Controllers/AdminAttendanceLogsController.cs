using Application.Features.AttendanceLogs.Queries.GetAdminList;
using Core.Application.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/admin/attendance-logs")]
[ApiController]
public class AdminAttendanceLogsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid? tenantId,
        [FromQuery] Guid? memberId,
        [FromQuery] int? gateId,
        [FromQuery] AttendanceResult? result,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to
    )
    {
        GetAdminListAttendanceLogQuery query =
            new()
            {
                PageRequest = pageRequest,
                TenantId = tenantId,
                MemberId = memberId,
                GateId = gateId,
                Result = result,
                From = from,
                To = to
            };

        var response = await Mediator.Send(query);
        return Ok(response);
    }
}
