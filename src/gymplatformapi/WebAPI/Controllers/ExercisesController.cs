using Application.Features.Exercises.Commands.Create;
using Application.Features.Exercises.Commands.Delete;
using Application.Features.Exercises.Commands.Update;
using Application.Features.Exercises.Queries.GetById;
using Application.Features.Exercises.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExercisesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateExerciseCommand createExerciseCommand)
    {
        CreatedExerciseResponse response = await Mediator.Send(createExerciseCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateExerciseCommand updateExerciseCommand)
    {
        UpdatedExerciseResponse response = await Mediator.Send(updateExerciseCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedExerciseResponse response = await Mediator.Send(new DeleteExerciseCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdExerciseResponse response = await Mediator.Send(new GetByIdExerciseQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListExerciseQuery getListExerciseQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListExerciseListItemDto> response = await Mediator.Send(getListExerciseQuery);
        return Ok(response);
    }
}
