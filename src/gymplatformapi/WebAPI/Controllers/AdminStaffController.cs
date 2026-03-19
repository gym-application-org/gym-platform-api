using Application.Features.Staffs.Queries.GetByIdAdmin;
using Application.Features.Staffs.Queries.GetListAdmin;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/admin/staffs")]
[ApiController]
public class AdminStaffController : BaseController
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAdminStaffResponse response = await Mediator.Send(new GetByIdAdminStaffQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromQuery] Guid? tenantId)
    {
        GetListAdminStaffQuery query = new() { PageRequest = pageRequest, TenantId = tenantId };
        GetListResponse<GetListAdminStaffListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}
