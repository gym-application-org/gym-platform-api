using Application.Features.WorkoutTemplateDayExercises.Commands.Create;
using Application.Features.WorkoutTemplateDayExercises.Commands.Delete;
using Application.Features.WorkoutTemplateDayExercises.Commands.Update;
using Application.Features.WorkoutTemplateDayExercises.Queries.GetById;
using Application.Features.WorkoutTemplateDayExercises.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutTemplateDayExercisesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateWorkoutTemplateDayExerciseCommand createWorkoutTemplateDayExerciseCommand)
    {
        CreatedWorkoutTemplateDayExerciseResponse response = await Mediator.Send(createWorkoutTemplateDayExerciseCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWorkoutTemplateDayExerciseCommand updateWorkoutTemplateDayExerciseCommand)
    {
        UpdatedWorkoutTemplateDayExerciseResponse response = await Mediator.Send(updateWorkoutTemplateDayExerciseCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedWorkoutTemplateDayExerciseResponse response = await Mediator.Send(new DeleteWorkoutTemplateDayExerciseCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdWorkoutTemplateDayExerciseResponse response = await Mediator.Send(new GetByIdWorkoutTemplateDayExerciseQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListWorkoutTemplateDayExerciseQuery getListWorkoutTemplateDayExerciseQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListWorkoutTemplateDayExerciseListItemDto> response = await Mediator.Send(
            getListWorkoutTemplateDayExerciseQuery
        );
        return Ok(response);
    }
}
