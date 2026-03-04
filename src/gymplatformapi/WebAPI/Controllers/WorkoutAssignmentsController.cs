using Application.Features.WorkoutAssignments.Commands.Create;
using Application.Features.WorkoutAssignments.Commands.CreateBulk;
using Application.Features.WorkoutAssignments.Commands.Delete;
using Application.Features.WorkoutAssignments.Commands.Update;
using Application.Features.WorkoutAssignments.Queries.GetById;
using Application.Features.WorkoutAssignments.Queries.GetList;
using Application.Features.WorkoutAssignments.Queries.GetMyWorkoutAssignmentList;
using Application.Features.WorkoutAssignments.Queries.GetMyWorkoutById;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/workout-assignments")]
[ApiController]
public class WorkoutAssignmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateWorkoutAssignmentCommand createWorkoutAssignmentCommand)
    {
        CreatedWorkoutAssignmentResponse response = await Mediator.Send(createWorkoutAssignmentCommand);

        return Created(uri: "", response);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> AddRange([FromBody] CreateBulkWorkoutAssignmentCommand createWorkoutAssignmentCommand)
    {
        CreatedBulkWorkoutAssignmentResponse response = await Mediator.Send(createWorkoutAssignmentCommand);

        return Created(uri: "", response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkoutAssignmentCommand updateWorkoutAssignmentCommand)
    {
        updateWorkoutAssignmentCommand.Id = id;
        UpdatedWorkoutAssignmentResponse response = await Mediator.Send(updateWorkoutAssignmentCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedWorkoutAssignmentResponse response = await Mediator.Send(new DeleteWorkoutAssignmentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdWorkoutAssignmentResponse response = await Mediator.Send(new GetByIdWorkoutAssignmentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListWorkoutAssignmentQuery getListWorkoutAssignmentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListWorkoutAssignmentListItemDto> response = await Mediator.Send(getListWorkoutAssignmentQuery);
        return Ok(response);
    }

    [HttpGet("me/{id:int}")]
    public async Task<IActionResult> GetMeById([FromRoute] int id)
    {
        GetMyWorkoutByIdResponse response = await Mediator.Send(new GetMyByIdWorkoutQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] AssignmentStatus? status
    )
    {
        GetMyListWorkoutAssignmentQuery getListWorkoutAssignmentQuery =
            new()
            {
                PageRequest = pageRequest,
                From = from,
                To = to,
                Status = status
            };
        GetListResponse<GetMyListWorkoutAssignmentListItemDto> response = await Mediator.Send(getListWorkoutAssignmentQuery);
        return Ok(response);
    }
}
