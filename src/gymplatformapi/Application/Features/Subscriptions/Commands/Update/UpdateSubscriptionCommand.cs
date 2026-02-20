using Application.Features.Subscriptions.Constants;
using Application.Features.Subscriptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Subscriptions.Constants.SubscriptionsOperationClaims;

namespace Application.Features.Subscriptions.Commands.Update;

public class UpdateSubscriptionCommand
    : IRequest<UpdatedSubscriptionResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionStatus Status { get; set; }
    public string PurchasedPlanName { get; set; }
    public int PurchasedDurationDays { get; set; }
    public decimal PurchasedUnitPrice { get; set; }
    public string Currency { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalPaid { get; set; }
    public Guid MemberId { get; set; }
    public int SubscriptionPlanId { get; set; }

    public string[] Roles => [Admin, Write, SubscriptionsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubscriptions"];

    public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, UpdatedSubscriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly SubscriptionBusinessRules _subscriptionBusinessRules;

        public UpdateSubscriptionCommandHandler(
            IMapper mapper,
            ISubscriptionRepository subscriptionRepository,
            SubscriptionBusinessRules subscriptionBusinessRules
        )
        {
            _mapper = mapper;
            _subscriptionRepository = subscriptionRepository;
            _subscriptionBusinessRules = subscriptionBusinessRules;
        }

        public async Task<UpdatedSubscriptionResponse> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            Subscription? subscription = await _subscriptionRepository.GetAsync(
                predicate: s => s.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _subscriptionBusinessRules.SubscriptionShouldExistWhenSelected(subscription);
            subscription = _mapper.Map(request, subscription);

            await _subscriptionRepository.UpdateAsync(subscription!);

            UpdatedSubscriptionResponse response = _mapper.Map<UpdatedSubscriptionResponse>(subscription);
            return response;
        }
    }
}
