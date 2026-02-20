using Application.Features.DietTemplateMealItems.Commands.Create;
using Application.Features.DietTemplateMealItems.Commands.Delete;
using Application.Features.DietTemplateMealItems.Commands.Update;
using Application.Features.DietTemplateMealItems.Queries.GetById;
using Application.Features.DietTemplateMealItems.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DietTemplateMealItemsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDietTemplateMealItemCommand createDietTemplateMealItemCommand)
    {
        CreatedDietTemplateMealItemResponse response = await Mediator.Send(createDietTemplateMealItemCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDietTemplateMealItemCommand updateDietTemplateMealItemCommand)
    {
        UpdatedDietTemplateMealItemResponse response = await Mediator.Send(updateDietTemplateMealItemCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedDietTemplateMealItemResponse response = await Mediator.Send(new DeleteDietTemplateMealItemCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdDietTemplateMealItemResponse response = await Mediator.Send(new GetByIdDietTemplateMealItemQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDietTemplateMealItemQuery getListDietTemplateMealItemQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDietTemplateMealItemListItemDto> response = await Mediator.Send(getListDietTemplateMealItemQuery);
        return Ok(response);
    }
}
