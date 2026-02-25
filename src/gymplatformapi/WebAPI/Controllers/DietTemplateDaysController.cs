using Application.Features.DietTemplateDays.Commands.Create;
using Application.Features.DietTemplateDays.Commands.Delete;
using Application.Features.DietTemplateDays.Commands.Update;
using Application.Features.DietTemplateDays.Queries.GetById;
using Application.Features.DietTemplateDays.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/*
[Route("api/[controller]")]
[ApiController]
public class DietTemplateDaysController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDietTemplateDayCommand createDietTemplateDayCommand)
    {
        CreatedDietTemplateDayResponse response = await Mediator.Send(createDietTemplateDayCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDietTemplateDayCommand updateDietTemplateDayCommand)
    {
        UpdatedDietTemplateDayResponse response = await Mediator.Send(updateDietTemplateDayCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedDietTemplateDayResponse response = await Mediator.Send(new DeleteDietTemplateDayCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdDietTemplateDayResponse response = await Mediator.Send(new GetByIdDietTemplateDayQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDietTemplateDayQuery getListDietTemplateDayQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDietTemplateDayListItemDto> response = await Mediator.Send(getListDietTemplateDayQuery);
        return Ok(response);
    }
}
*/
