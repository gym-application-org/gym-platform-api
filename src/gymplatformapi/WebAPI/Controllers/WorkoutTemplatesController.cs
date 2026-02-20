using Application.Features.WorkoutTemplates.Commands.Create;
using Application.Features.WorkoutTemplates.Commands.Delete;
using Application.Features.WorkoutTemplates.Commands.Update;
using Application.Features.WorkoutTemplates.Queries.GetById;
using Application.Features.WorkoutTemplates.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutTemplatesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateWorkoutTemplateCommand createWorkoutTemplateCommand)
    {
        CreatedWorkoutTemplateResponse response = await Mediator.Send(createWorkoutTemplateCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWorkoutTemplateCommand updateWorkoutTemplateCommand)
    {
        UpdatedWorkoutTemplateResponse response = await Mediator.Send(updateWorkoutTemplateCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedWorkoutTemplateResponse response = await Mediator.Send(new DeleteWorkoutTemplateCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdWorkoutTemplateResponse response = await Mediator.Send(new GetByIdWorkoutTemplateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListWorkoutTemplateQuery getListWorkoutTemplateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListWorkoutTemplateListItemDto> response = await Mediator.Send(getListWorkoutTemplateQuery);
        return Ok(response);
    }
}
