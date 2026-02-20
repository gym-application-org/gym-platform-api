using Application.Features.DietTemplateMeals.Commands.Create;
using Application.Features.DietTemplateMeals.Commands.Delete;
using Application.Features.DietTemplateMeals.Commands.Update;
using Application.Features.DietTemplateMeals.Queries.GetById;
using Application.Features.DietTemplateMeals.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DietTemplateMealsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDietTemplateMealCommand createDietTemplateMealCommand)
    {
        CreatedDietTemplateMealResponse response = await Mediator.Send(createDietTemplateMealCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDietTemplateMealCommand updateDietTemplateMealCommand)
    {
        UpdatedDietTemplateMealResponse response = await Mediator.Send(updateDietTemplateMealCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedDietTemplateMealResponse response = await Mediator.Send(new DeleteDietTemplateMealCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdDietTemplateMealResponse response = await Mediator.Send(new GetByIdDietTemplateMealQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDietTemplateMealQuery getListDietTemplateMealQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDietTemplateMealListItemDto> response = await Mediator.Send(getListDietTemplateMealQuery);
        return Ok(response);
    }
}
