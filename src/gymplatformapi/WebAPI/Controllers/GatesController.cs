using Application.Features.Gates.Commands.Create;
using Application.Features.Gates.Commands.Delete;
using Application.Features.Gates.Commands.Update;
using Application.Features.Gates.Queries.GetById;
using Application.Features.Gates.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GatesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateGateCommand createGateCommand)
    {
        CreatedGateResponse response = await Mediator.Send(createGateCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateGateCommand updateGateCommand)
    {
        UpdatedGateResponse response = await Mediator.Send(updateGateCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedGateResponse response = await Mediator.Send(new DeleteGateCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdGateResponse response = await Mediator.Send(new GetByIdGateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListGateQuery getListGateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListGateListItemDto> response = await Mediator.Send(getListGateQuery);
        return Ok(response);
    }
}
