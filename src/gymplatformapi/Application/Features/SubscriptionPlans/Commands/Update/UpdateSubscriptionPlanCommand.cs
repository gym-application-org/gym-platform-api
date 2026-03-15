using Application.Features.SubscriptionPlans.Constants;
using Application.Features.SubscriptionPlans.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.SubscriptionPlans.Constants.SubscriptionPlansOperationClaims;

namespace Application.Features.SubscriptionPlans.Commands.Update;

public class UpdateSubscriptionPlanCommand
    : IRequest<UpdatedSubscriptionPlanResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner, GeneralOperationClaims.Staff];

    public class UpdateSubscriptionPlanCommandHandler : IRequestHandler<UpdateSubscriptionPlanCommand, UpdatedSubscriptionPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly SubscriptionPlanBusinessRules _subscriptionPlanBusinessRules;
        private readonly ICurrentTenant _currentTenant;

        public UpdateSubscriptionPlanCommandHandler(
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

        public async Task<UpdatedSubscriptionPlanResponse> Handle(
            UpdateSubscriptionPlanCommand request,
            CancellationToken cancellationToken
        )
        {
            SubscriptionPlan? subscriptionPlan = await _subscriptionPlanRepository.GetAsync(
                predicate: sp => sp.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _subscriptionPlanBusinessRules.SubscriptionPlanShouldExistWhenSelected(subscriptionPlan);
            subscriptionPlan = _mapper.Map(request, subscriptionPlan);
            subscriptionPlan!.TenantId = _currentTenant.TenantId!.Value;

            await _subscriptionPlanRepository.UpdateAsync(subscriptionPlan!);

            UpdatedSubscriptionPlanResponse response = _mapper.Map<UpdatedSubscriptionPlanResponse>(subscriptionPlan);
            return response;
        }
    }
}
