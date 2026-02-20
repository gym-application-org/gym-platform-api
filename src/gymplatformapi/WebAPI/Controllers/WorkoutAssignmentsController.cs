using Application.Features.WorkoutAssignments.Commands.Create;
using Application.Features.WorkoutAssignments.Commands.Delete;
using Application.Features.WorkoutAssignments.Commands.Update;
using Application.Features.WorkoutAssignments.Queries.GetById;
using Application.Features.WorkoutAssignments.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutAssignmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateWorkoutAssignmentCommand createWorkoutAssignmentCommand)
    {
        CreatedWorkoutAssignmentResponse response = await Mediator.Send(createWorkoutAssignmentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWorkoutAssignmentCommand updateWorkoutAssignmentCommand)
    {
        UpdatedWorkoutAssignmentResponse response = await Mediator.Send(updateWorkoutAssignmentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedWorkoutAssignmentResponse response = await Mediator.Send(new DeleteWorkoutAssignmentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
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
}
