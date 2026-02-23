using Application.Features.SupportTickets.Commands.Delete;
using Application.Features.SupportTickets.Commands.DeleteAdmin;
using Application.Features.SupportTickets.Commands.Update;
using Application.Features.SupportTickets.Commands.UpdateAdmin;
using Application.Features.SupportTickets.Queries.GetAdminById;
using Application.Features.SupportTickets.Queries.GetAdminList;
using Application.Features.SupportTickets.Queries.GetById;
using Application.Features.SupportTickets.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/admin/support-tickets")]
[ApiController]
public class AdminSupportTicketsController : BaseController
{
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAdminSupportTicketCommand updateSupportTicketCommand)
    {
        updateSupportTicketCommand.Id = id;
        UpdatedAdminSupportTicketResponse response = await Mediator.Send(updateSupportTicketCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedAdminSupportTicketResponse response = await Mediator.Send(new DeleteAdminSupportTicketCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetAdminByIdSupportTicketResponse response = await Mediator.Send(new GetAdminByIdSupportTicketQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] DateTime? from,
        DateTime? to,
        TicketStatus? status,
        TicketPriority? priority,
        Guid? tenantId
    )
    {
        GetAdminListSupportTicketQuery getListSupportTicketQuery =
            new()
            {
                PageRequest = pageRequest,
                From = from,
                To = to,
                Priority = priority,
                Status = status,
                TenantId = tenantId
            };
        GetListResponse<GetAdminListSupportTicketListItemDto> response = await Mediator.Send(getListSupportTicketQuery);
        return Ok(response);
    }
}
