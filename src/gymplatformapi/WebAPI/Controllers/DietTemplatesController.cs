using Application.Features.DietTemplates.Commands.Create;
using Application.Features.DietTemplates.Commands.Delete;
using Application.Features.DietTemplates.Commands.Update;
using Application.Features.DietTemplates.Queries.GetById;
using Application.Features.DietTemplates.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/diet-templates")]
[ApiController]
public class DietTemplatesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDietTemplateCommand createDietTemplateCommand)
    {
        CreatedDietTemplateResponse response = await Mediator.Send(createDietTemplateCommand);

        return Created(uri: "", response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDietTemplateCommand updateDietTemplateCommand)
    {
        updateDietTemplateCommand.Id = id;
        UpdatedDietTemplateResponse response = await Mediator.Send(updateDietTemplateCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedDietTemplateResponse response = await Mediator.Send(new DeleteDietTemplateCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdDietTemplateResponse response = await Mediator.Send(new GetByIdDietTemplateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDietTemplateQuery getListDietTemplateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDietTemplateListItemDto> response = await Mediator.Send(getListDietTemplateQuery);
        return Ok(response);
    }
}
