using Application.Features.WorkoutTemplateDays.Commands.Create;
using Application.Features.WorkoutTemplateDays.Commands.Delete;
using Application.Features.WorkoutTemplateDays.Commands.Update;
using Application.Features.WorkoutTemplateDays.Queries.GetById;
using Application.Features.WorkoutTemplateDays.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutTemplateDaysController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateWorkoutTemplateDayCommand createWorkoutTemplateDayCommand)
    {
        CreatedWorkoutTemplateDayResponse response = await Mediator.Send(createWorkoutTemplateDayCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWorkoutTemplateDayCommand updateWorkoutTemplateDayCommand)
    {
        UpdatedWorkoutTemplateDayResponse response = await Mediator.Send(updateWorkoutTemplateDayCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedWorkoutTemplateDayResponse response = await Mediator.Send(new DeleteWorkoutTemplateDayCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdWorkoutTemplateDayResponse response = await Mediator.Send(new GetByIdWorkoutTemplateDayQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListWorkoutTemplateDayQuery getListWorkoutTemplateDayQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListWorkoutTemplateDayListItemDto> response = await Mediator.Send(getListWorkoutTemplateDayQuery);
        return Ok(response);
    }
}
