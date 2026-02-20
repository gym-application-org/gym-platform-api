using Application.Features.Staffs.Commands.Create;
using Application.Features.Staffs.Commands.Delete;
using Application.Features.Staffs.Commands.Update;
using Application.Features.Staffs.Queries.GetById;
using Application.Features.Staffs.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStaffCommand createStaffCommand)
    {
        CreatedStaffResponse response = await Mediator.Send(createStaffCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStaffCommand updateStaffCommand)
    {
        UpdatedStaffResponse response = await Mediator.Send(updateStaffCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStaffResponse response = await Mediator.Send(new DeleteStaffCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStaffResponse response = await Mediator.Send(new GetByIdStaffQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStaffQuery getListStaffQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStaffListItemDto> response = await Mediator.Send(getListStaffQuery);
        return Ok(response);
    }
}
