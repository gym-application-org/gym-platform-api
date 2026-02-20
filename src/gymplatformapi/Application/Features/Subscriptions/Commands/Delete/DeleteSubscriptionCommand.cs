using Application.Features.Subscriptions.Constants;
using Application.Features.Subscriptions.Constants;
using Application.Features.Subscriptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.Subscriptions.Constants.SubscriptionsOperationClaims;

namespace Application.Features.Subscriptions.Commands.Delete;

public class DeleteSubscriptionCommand
    : IRequest<DeletedSubscriptionResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, SubscriptionsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubscriptions"];

    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, DeletedSubscriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly SubscriptionBusinessRules _subscriptionBusinessRules;

        public DeleteSubscriptionCommandHandler(
            IMapper mapper,
            ISubscriptionRepository subscriptionRepository,
            SubscriptionBusinessRules subscriptionBusinessRules
        )
        {
            _mapper = mapper;
            _subscriptionRepository = subscriptionRepository;
            _subscriptionBusinessRules = subscriptionBusinessRules;
        }

        public async Task<DeletedSubscriptionResponse> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            Subscription? subscription = await _subscriptionRepository.GetAsync(
                predicate: s => s.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _subscriptionBusinessRules.SubscriptionShouldExistWhenSelected(subscription);

            await _subscriptionRepository.DeleteAsync(subscription!);

            DeletedSubscriptionResponse response = _mapper.Map<DeletedSubscriptionResponse>(subscription);
            return response;
        }
    }
}
