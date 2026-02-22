using Application.Features.SubscriptionPlans.Constants;
using Application.Features.SubscriptionPlans.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.SubscriptionPlans.Constants.SubscriptionPlansOperationClaims;

namespace Application.Features.SubscriptionPlans.Commands.Create;

public class CreateSubscriptionPlanCommand
    : IRequest<CreatedSubscriptionPlanResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public string Name { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubscriptionPlans"];

    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, CreatedSubscriptionPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly SubscriptionPlanBusinessRules _subscriptionPlanBusinessRules;
        private readonly ICurrentTenant _currentTenant;

        public CreateSubscriptionPlanCommandHandler(
            IMapper mapper,
            ISubscriptionPlanRepository subscriptionPlanRepository,
            SubscriptionPlanBusinessRules subscriptionPlanBusinessRules,
            ICurrentTenant currentTenant
        )
        {
            _mapper = mapper;
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _subscriptionPlanBusinessRules = subscriptionPlanBusinessRules;
            _currentTenant = currentTenant;
        }

        public async Task<CreatedSubscriptionPlanResponse> Handle(
            CreateSubscriptionPlanCommand request,
            CancellationToken cancellationToken
        )
        {
            SubscriptionPlan subscriptionPlan = _mapper.Map<SubscriptionPlan>(request);
            subscriptionPlan.TenantId = _currentTenant.TenantId!.Value;

            await _subscriptionPlanRepository.AddAsync(subscriptionPlan);

            CreatedSubscriptionPlanResponse response = _mapper.Map<CreatedSubscriptionPlanResponse>(subscriptionPlan);
            return response;
        }
    }
}
