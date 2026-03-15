using Application.Features.Staffs.Queries.GetByIdAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/admin/staffs")]
[ApiController]
public class AdminStaffController : BaseController
{
    [HttpGet("{id:guid")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAdminStaffResponse response = await Mediator.Send(new GetByIdAdminStaffQuery { Id = id });
        return Ok(response);
    }
}
