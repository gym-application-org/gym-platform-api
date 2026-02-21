using Application.Features.ProgressEntries.Commands.Create;
using Application.Features.ProgressEntries.Commands.Delete;
using Application.Features.ProgressEntries.Commands.Update;
using Application.Features.ProgressEntries.Queries.GetById;
using Application.Features.ProgressEntries.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/progress-entries")]
[ApiController]
public class ProgressEntriesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProgressEntryCommand createProgressEntryCommand)
    {
        CreatedProgressEntryResponse response = await Mediator.Send(createProgressEntryCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProgressEntryCommand updateProgressEntryCommand)
    {
        UpdatedProgressEntryResponse response = await Mediator.Send(updateProgressEntryCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedProgressEntryResponse response = await Mediator.Send(new DeleteProgressEntryCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdProgressEntryResponse response = await Mediator.Send(new GetByIdProgressEntryQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        GetListProgressEntryQuery getListProgressEntryQuery =
            new()
            {
                PageRequest = pageRequest,
                From = from,
                To = to
            };
        GetListResponse<GetListProgressEntryListItemDto> response = await Mediator.Send(getListProgressEntryQuery);
        return Ok(response);
    }
}
