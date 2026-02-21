using Application.Features.Gates.Queries.GetByIdTenant;
using Application.Features.Gates.Queries.GetListTenant;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/gates")]
[ApiController]
public class GatesController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdTenantGateResponse response = await Mediator.Send(new GetByIdTenantGateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTenantGateQuery getListGateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTenantGateListItemDto> response = await Mediator.Send(getListGateQuery);
        return Ok(response);
    }
}
