using Application.Features.DietAssignments.Commands.Create;
using Application.Features.DietAssignments.Commands.CreateBulk;
using Application.Features.DietAssignments.Commands.Delete;
using Application.Features.DietAssignments.Commands.Update;
using Application.Features.DietAssignments.Queries.GetById;
using Application.Features.DietAssignments.Queries.GetList;
using Application.Features.DietAssignments.Queries.GetMyDietAssignemnts;
using Application.Features.DietAssignments.Queries.GetMyDietAssignmentById;
using Application.Features.DietAssignments.Queries.GetMyDietAssignments;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/diet-assignments")]
[ApiController]
public class DietAssignmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDietAssignmentCommand createDietAssignmentCommand)
    {
        CreatedDietAssignmentResponse response = await Mediator.Send(createDietAssignmentCommand);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> AddRange([FromBody] CreateBulkDietAssignmentCommand createDietAssignmentCommand)
    {
        CreatedBulkDietAssignmentResponse response = await Mediator.Send(createDietAssignmentCommand);

        return Created(uri: "", response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDietAssignmentCommand updateDietAssignmentCommand)
    {
        updateDietAssignmentCommand.Id = id;
        UpdatedDietAssignmentResponse response = await Mediator.Send(updateDietAssignmentCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedDietAssignmentResponse response = await Mediator.Send(new DeleteDietAssignmentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdDietAssignmentResponse response = await Mediator.Send(new GetByIdDietAssignmentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] AssignmentStatus? status
    )
    {
        GetListDietAssignmentQuery getListDietAssignmentQuery =
            new()
            {
                PageRequest = pageRequest,
                From = from,
                To = to,
                Status = status
            };
        GetListResponse<GetListDietAssignmentListItemDto> response = await Mediator.Send(getListDietAssignmentQuery);
        return Ok(response);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMeList([FromQuery] PageRequest pageRequest, [FromQuery] AssignmentStatus? status)
    {
        GetMyDietAssignmentsListQuery getListDietAssignmentQuery = new() { PageRequest = pageRequest, Status = status };
        GetListResponse<GetMyDietAssignmentsListItemDto> response = await Mediator.Send(getListDietAssignmentQuery);
        return Ok(response);
    }

    [HttpGet("me/{id:int}")]
    public async Task<IActionResult> GetMeActiveById([FromRoute] int id)
    {
        GetMyDietAssignmentByIdResponse response = await Mediator.Send(new GetMyDietAssignmentByIdQuery { Id = id });
        return Ok(response);
    }
}
