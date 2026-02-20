using Application.Features.AttendanceLogs.Commands.Create;
using Application.Features.AttendanceLogs.Queries.GetById;
using Application.Features.AttendanceLogs.Queries.GetByMemberList;
using Application.Features.AttendanceLogs.Queries.GetList;
using Application.Features.AttendanceLogs.Queries.GetMyList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceLogsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAttendanceLogCommand createAttendanceLogCommand)
    {
        CreatedAttendanceLogResponse response = await Mediator.Send(createAttendanceLogCommand);

        return Created(uri: "", response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdAttendanceLogResponse response = await Mediator.Send(new GetByIdAttendanceLogQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] int? gateId,
        [FromQuery] Domain.Enums.AttendanceResult? result,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to
    )
    {
        GetListAttendanceLogQuery getListAttendanceLogQuery =
            new()
            {
                PageRequest = pageRequest,
                GateId = gateId,
                Result = result,
                From = from,
                To = to
            };
        GetListResponse<GetListAttendanceLogListItemDto> response = await Mediator.Send(getListAttendanceLogQuery);
        return Ok(response);
    }

    [HttpGet("member/{memberId:Guid}")]
    public async Task<IActionResult> GetByMember(
        [FromRoute] Guid memberId,
        [FromQuery] PageRequest pageRequest,
        [FromQuery] int? gateId,
        [FromQuery] Domain.Enums.AttendanceResult? result,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to
    )
    {
        GetByMemberListAttendanceLogQuery query =
            new()
            {
                MemberId = memberId,
                PageRequest = pageRequest,
                GateId = gateId,
                From = from,
                To = to,
                Result = result
            };

        GetListResponse<GetByMemberListAttendanceLogListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] int? gateId,
        [FromQuery] Domain.Enums.AttendanceResult? result,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to
    )
    {
        GetMyListAttendanceLogQuery query =
            new()
            {
                PageRequest = pageRequest,
                GateId = gateId,
                From = from,
                To = to,
                Result = result
            };

        GetListResponse<GetMyListAttendanceLogListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}
