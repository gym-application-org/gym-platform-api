using Application.Features.Gates.Commands.Create;
using Application.Features.Gates.Commands.Delete;
using Application.Features.Gates.Commands.Update;
using Application.Features.Gates.Queries.GetByIdAdmin;
using Application.Features.Gates.Queries.GetByIdTenant;
using Application.Features.Gates.Queries.GetListAdmin;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/admin/gates")]
[ApiController]
public class AdminGatesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateGateCommand createGateCommand)
    {
        CreatedGateResponse response = await Mediator.Send(createGateCommand);

        return Created(uri: "", response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGateCommand updateGateCommand)
    {
        updateGateCommand.Id = id;
        UpdatedGateResponse response = await Mediator.Send(updateGateCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedGateResponse response = await Mediator.Send(new DeleteGateCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdGateResponse response = await Mediator.Send(new GetByIdGateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] Guid? tenantId, [FromQuery] PageRequest pageRequest)
    {
        GetListGateQuery getListGateQuery = new() { PageRequest = pageRequest, TenantId = tenantId };
        GetListResponse<GetListGateListItemDto> response = await Mediator.Send(getListGateQuery);
        return Ok(response);
    }
}
