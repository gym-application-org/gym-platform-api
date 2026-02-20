using Application.Features.AttendanceLogs.Commands.Create;
using Application.Features.AttendanceLogs.Commands.Delete;
using Application.Features.AttendanceLogs.Commands.Update;
using Application.Features.AttendanceLogs.Queries.GetById;
using Application.Features.AttendanceLogs.Queries.GetList;
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

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAttendanceLogCommand updateAttendanceLogCommand)
    {
        UpdatedAttendanceLogResponse response = await Mediator.Send(updateAttendanceLogCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedAttendanceLogResponse response = await Mediator.Send(new DeleteAttendanceLogCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdAttendanceLogResponse response = await Mediator.Send(new GetByIdAttendanceLogQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAttendanceLogQuery getListAttendanceLogQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAttendanceLogListItemDto> response = await Mediator.Send(getListAttendanceLogQuery);
        return Ok(response);
    }
}
