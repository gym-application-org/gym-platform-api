using Application.Features.Tenants.Queries.GetList;
using Application.Features.Tenants.Queries.GetMy;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/tenants")]
[ApiController]
public class TenantsController : BaseController
{
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        GetMyTenantQuery getListTenantQuery = new() { };
        GetMyTenantResponse response = await Mediator.Send(getListTenantQuery);
        return Ok(response);
    }
}
