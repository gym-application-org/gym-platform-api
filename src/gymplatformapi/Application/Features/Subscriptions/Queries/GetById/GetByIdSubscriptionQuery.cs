using Application.Features.Subscriptions.Constants;
using Application.Features.Subscriptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Subscriptions.Constants.SubscriptionsOperationClaims;

namespace Application.Features.Subscriptions.Queries.GetById;

public class GetByIdSubscriptionQuery : IRequest<GetByIdSubscriptionResponse>, ISecuredRequest, ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetByIdSubscriptionQueryHandler : IRequestHandler<GetByIdSubscriptionQuery, GetByIdSubscriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly SubscriptionBusinessRules _subscriptionBusinessRules;

        public GetByIdSubscriptionQueryHandler(
            IMapper mapper,
            ISubscriptionRepository subscriptionRepository,
            SubscriptionBusinessRules subscriptionBusinessRules
        )
        {
            _mapper = mapper;
            _subscriptionRepository = subscriptionRepository;
            _subscriptionBusinessRules = subscriptionBusinessRules;
        }

        public async Task<GetByIdSubscriptionResponse> Handle(GetByIdSubscriptionQuery request, CancellationToken cancellationToken)
        {
            Subscription? subscription = await _subscriptionRepository.GetAsync(
                predicate: s => s.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _subscriptionBusinessRules.SubscriptionShouldExistWhenSelected(subscription);

            GetByIdSubscriptionResponse response = _mapper.Map<GetByIdSubscriptionResponse>(subscription);
            return response;
        }
    }
}
