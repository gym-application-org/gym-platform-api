using Application.Features.DietAssignments.Commands.Create;
using Application.Features.DietAssignments.Commands.Delete;
using Application.Features.DietAssignments.Commands.Update;
using Application.Features.DietAssignments.Queries.GetById;
using Application.Features.DietAssignments.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DietAssignmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDietAssignmentCommand createDietAssignmentCommand)
    {
        CreatedDietAssignmentResponse response = await Mediator.Send(createDietAssignmentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDietAssignmentCommand updateDietAssignmentCommand)
    {
        UpdatedDietAssignmentResponse response = await Mediator.Send(updateDietAssignmentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedDietAssignmentResponse response = await Mediator.Send(new DeleteDietAssignmentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdDietAssignmentResponse response = await Mediator.Send(new GetByIdDietAssignmentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDietAssignmentQuery getListDietAssignmentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDietAssignmentListItemDto> response = await Mediator.Send(getListDietAssignmentQuery);
        return Ok(response);
    }
}
