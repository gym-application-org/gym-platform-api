using Application.Features.SubscriptionPlans.Commands.Create;
using Application.Features.SubscriptionPlans.Commands.Delete;
using Application.Features.SubscriptionPlans.Commands.Update;
using Application.Features.SubscriptionPlans.Queries.GetById;
using Application.Features.SubscriptionPlans.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionPlansController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSubscriptionPlanCommand createSubscriptionPlanCommand)
    {
        CreatedSubscriptionPlanResponse response = await Mediator.Send(createSubscriptionPlanCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSubscriptionPlanCommand updateSubscriptionPlanCommand)
    {
        UpdatedSubscriptionPlanResponse response = await Mediator.Send(updateSubscriptionPlanCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedSubscriptionPlanResponse response = await Mediator.Send(new DeleteSubscriptionPlanCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdSubscriptionPlanResponse response = await Mediator.Send(new GetByIdSubscriptionPlanQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSubscriptionPlanQuery getListSubscriptionPlanQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSubscriptionPlanListItemDto> response = await Mediator.Send(getListSubscriptionPlanQuery);
        return Ok(response);
    }
}
