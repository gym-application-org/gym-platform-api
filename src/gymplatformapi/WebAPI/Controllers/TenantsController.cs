using Application.Features.Tenants.Commands.Create;
using Application.Features.Tenants.Commands.Delete;
using Application.Features.Tenants.Commands.Update;
using Application.Features.Tenants.Queries.GetById;
using Application.Features.Tenants.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TenantsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTenantCommand createTenantCommand)
    {
        CreatedTenantResponse response = await Mediator.Send(createTenantCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTenantCommand updateTenantCommand)
    {
        UpdatedTenantResponse response = await Mediator.Send(updateTenantCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTenantResponse response = await Mediator.Send(new DeleteTenantCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTenantResponse response = await Mediator.Send(new GetByIdTenantQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTenantQuery getListTenantQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTenantListItemDto> response = await Mediator.Send(getListTenantQuery);
        return Ok(response);
    }
}
