using Application.Features.SubscriptionPlans.Constants;
using Application.Features.SubscriptionPlans.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.SubscriptionPlans.Constants.SubscriptionPlansOperationClaims;

namespace Application.Features.SubscriptionPlans.Commands.Create;

public class CreateSubscriptionPlanCommand
    : IRequest<CreatedSubscriptionPlanResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string Name { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, SubscriptionPlansOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubscriptionPlans"];

    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, CreatedSubscriptionPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly SubscriptionPlanBusinessRules _subscriptionPlanBusinessRules;

        public CreateSubscriptionPlanCommandHandler(
            IMapper mapper,
            ISubscriptionPlanRepository subscriptionPlanRepository,
            SubscriptionPlanBusinessRules subscriptionPlanBusinessRules
        )
        {
            _mapper = mapper;
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _subscriptionPlanBusinessRules = subscriptionPlanBusinessRules;
        }

        public async Task<CreatedSubscriptionPlanResponse> Handle(
            CreateSubscriptionPlanCommand request,
            CancellationToken cancellationToken
        )
        {
            SubscriptionPlan subscriptionPlan = _mapper.Map<SubscriptionPlan>(request);

            await _subscriptionPlanRepository.AddAsync(subscriptionPlan);

            CreatedSubscriptionPlanResponse response = _mapper.Map<CreatedSubscriptionPlanResponse>(subscriptionPlan);
            return response;
        }
    }
}
