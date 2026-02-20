using Application.Features.SupportTickets.Commands.Create;
using Application.Features.SupportTickets.Commands.Delete;
using Application.Features.SupportTickets.Commands.Update;
using Application.Features.SupportTickets.Queries.GetById;
using Application.Features.SupportTickets.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupportTicketsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSupportTicketCommand createSupportTicketCommand)
    {
        CreatedSupportTicketResponse response = await Mediator.Send(createSupportTicketCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSupportTicketCommand updateSupportTicketCommand)
    {
        UpdatedSupportTicketResponse response = await Mediator.Send(updateSupportTicketCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedSupportTicketResponse response = await Mediator.Send(new DeleteSupportTicketCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdSupportTicketResponse response = await Mediator.Send(new GetByIdSupportTicketQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSupportTicketQuery getListSupportTicketQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSupportTicketListItemDto> response = await Mediator.Send(getListSupportTicketQuery);
        return Ok(response);
    }
}
