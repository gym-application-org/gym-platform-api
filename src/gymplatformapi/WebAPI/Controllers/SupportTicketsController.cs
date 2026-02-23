using Application.Features.SupportTickets.Commands.Create;
using Application.Features.SupportTickets.Commands.Delete;
using Application.Features.SupportTickets.Commands.Update;
using Application.Features.SupportTickets.Queries.GetById;
using Application.Features.SupportTickets.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/support-tickets")]
[ApiController]
public class SupportTicketsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSupportTicketCommand createSupportTicketCommand)
    {
        CreatedSupportTicketResponse response = await Mediator.Send(createSupportTicketCommand);

        return Created(uri: "", response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSupportTicketCommand updateSupportTicketCommand)
    {
        updateSupportTicketCommand.Id = id;
        UpdatedSupportTicketResponse response = await Mediator.Send(updateSupportTicketCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedSupportTicketResponse response = await Mediator.Send(new DeleteSupportTicketCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdSupportTicketResponse response = await Mediator.Send(new GetByIdSupportTicketQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] DateTime? from,
        DateTime? to,
        TicketStatus? status,
        TicketPriority? priority
    )
    {
        GetListSupportTicketQuery getListSupportTicketQuery =
            new()
            {
                PageRequest = pageRequest,
                From = from,
                To = to,
                Priority = priority,
                Status = status
            };
        GetListResponse<GetListSupportTicketListItemDto> response = await Mediator.Send(getListSupportTicketQuery);
        return Ok(response);
    }
}
