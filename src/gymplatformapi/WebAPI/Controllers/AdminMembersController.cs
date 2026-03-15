using Application.Features.Members.Queries.GetByIdAdmin;
using Application.Features.Members.Queries.GetListAdmin;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/admin/members")]
[ApiController]
public class AdminMembersController : BaseController
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAdminMemberResponse response = await Mediator.Send(new GetByIdAdminMemberQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] MemberStatus? status,
        [FromQuery] Guid? tenantId
    )
    {
        GetListAdminMemberQuery getListMemberQuery =
            new()
            {
                PageRequest = pageRequest,
                Status = status,
                TenantId = tenantId
            };
        GetListResponse<GetListAdminMemberListItemDto> response = await Mediator.Send(getListMemberQuery);
        return Ok(response);
    }
}
