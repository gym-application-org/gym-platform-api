using Application.Features.Exercises.Commands.Create;
using Application.Features.Exercises.Commands.Delete;
using Application.Features.Exercises.Commands.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/admin/exercises")]
[ApiController]
public class AdminExerciseController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateExerciseCommand createExerciseCommand)
    {
        CreatedExerciseResponse response = await Mediator.Send(createExerciseCommand);

        return CreatedAtAction(
            actionName: nameof(ExercisesController.GetById),
            controllerName: "Exercises",
            routeValues: new { id = response.Id },
            value: response
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateExerciseCommand updateExerciseCommand)
    {
        updateExerciseCommand.Id = id;
        UpdatedExerciseResponse response = await Mediator.Send(updateExerciseCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedExerciseResponse response = await Mediator.Send(new DeleteExerciseCommand { Id = id });

        return Ok(response);
    }
}
