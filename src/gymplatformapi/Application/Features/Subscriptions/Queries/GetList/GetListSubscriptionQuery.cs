using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Subscriptions.Constants.SubscriptionsOperationClaims;

namespace Application.Features.Subscriptions.Queries.GetList;

public class GetListSubscriptionQuery : IRequest<GetListResponse<GetListSubscriptionListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public Guid? MemberId { get; set; }

    public int? SubscriptionPlanId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetListSubscriptionQueryHandler
        : IRequestHandler<GetListSubscriptionQuery, GetListResponse<GetListSubscriptionListItemDto>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public GetListSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSubscriptionListItemDto>> Handle(
            GetListSubscriptionQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Subscription> subscriptions = await _subscriptionRepository.GetListAsync(
                predicate: x =>
                    (!request.From.HasValue || x.StartDate >= request.From.Value)
                    && (!request.To.HasValue || x.EndDate <= request.To.Value)
                    && (!request.MemberId.HasValue || x.MemberId == request.MemberId.Value)
                    && (!request.SubscriptionPlanId.HasValue || x.SubscriptionPlanId == request.SubscriptionPlanId.Value),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSubscriptionListItemDto> response = _mapper.Map<GetListResponse<GetListSubscriptionListItemDto>>(
                subscriptions
            );
            return response;
        }
    }
}
