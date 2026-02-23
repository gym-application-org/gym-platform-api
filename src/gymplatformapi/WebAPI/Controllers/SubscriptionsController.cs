using Application.Features.Subscriptions.Commands.Create;
using Application.Features.Subscriptions.Commands.Delete;
using Application.Features.Subscriptions.Commands.Update;
using Application.Features.Subscriptions.Queries.GetById;
using Application.Features.Subscriptions.Queries.GetList;
using Application.Features.Subscriptions.Queries.GetMy;
using Application.Features.Subscriptions.Queries.GetMyList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/subscriptions")]
[ApiController]
public class SubscriptionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSubscriptionCommand createSubscriptionCommand)
    {
        CreatedSubscriptionResponse response = await Mediator.Send(createSubscriptionCommand);

        return Created(uri: "", response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubscriptionCommand updateSubscriptionCommand)
    {
        updateSubscriptionCommand.Id = id;
        UpdatedSubscriptionResponse response = await Mediator.Send(updateSubscriptionCommand);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedSubscriptionResponse response = await Mediator.Send(new DeleteSubscriptionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdSubscriptionResponse response = await Mediator.Send(new GetByIdSubscriptionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int subscriptionPlanId,
        [FromQuery] Guid? memberId
    )
    {
        GetListSubscriptionQuery getListSubscriptionQuery =
            new()
            {
                PageRequest = pageRequest,
                From = from,
                To = to,
                MemberId = memberId,
                SubscriptionPlanId = subscriptionPlanId
            };
        GetListResponse<GetListSubscriptionListItemDto> response = await Mediator.Send(getListSubscriptionQuery);
        return Ok(response);
    }

    [HttpGet("me/{id}")]
    public async Task<IActionResult> GetMeById([FromRoute] int id)
    {
        GetMyByIdSubscriptionResponse response = await Mediator.Send(new GetMyByIdSubscriptionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMeList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int subscriptionPlanId
    )
    {
        GetMyListSubscriptionQuery getListSubscriptionQuery =
            new()
            {
                PageRequest = pageRequest,
                From = from,
                To = to,
                SubscriptionPlanId = subscriptionPlanId
            };
        GetListResponse<GetMyListSubscriptionListItemDto> response = await Mediator.Send(getListSubscriptionQuery);
        return Ok(response);
    }
}
