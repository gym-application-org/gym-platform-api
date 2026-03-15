using Application.Features.Subscriptions.Constants;
using Application.Features.Subscriptions.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using Application.Services.SubscriptionPlans;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Subscriptions.Constants.SubscriptionsOperationClaims;

namespace Application.Features.Subscriptions.Commands.Create;

public class CreateSubscriptionCommand
    : IRequest<CreatedSubscriptionResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
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

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, CreatedSubscriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly SubscriptionBusinessRules _subscriptionBusinessRules;
        private readonly ICurrentTenant _currentTenant;
        private readonly IMemberService _memberService;
        private readonly ISubscriptionPlanService _subscriptionPlanService;

        public CreateSubscriptionCommandHandler(
            IMapper mapper,
            ISubscriptionRepository subscriptionRepository,
            SubscriptionBusinessRules subscriptionBusinessRules,
            ICurrentTenant currentTenant,
            IMemberService memberService,
            ISubscriptionPlanService subscriptionPlanService
        )
        {
            _mapper = mapper;
            _subscriptionRepository = subscriptionRepository;
            _subscriptionBusinessRules = subscriptionBusinessRules;
            _currentTenant = currentTenant;
            _memberService = memberService;
            _subscriptionPlanService = subscriptionPlanService;
        }

        public async Task<CreatedSubscriptionResponse> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(x => x.Id == request.MemberId, cancellationToken: cancellationToken);
            await _subscriptionBusinessRules.MemberShouldExistWhenSelected(member);

            SubscriptionPlan? subscriptionPlan = await _subscriptionPlanService.GetAsync(
                x => x.Id == request.SubscriptionPlanId,
                cancellationToken: cancellationToken
            );
            await _subscriptionBusinessRules.SubscriptionPlanShouldExistWhenSelected(subscriptionPlan);

            Subscription subscription = _mapper.Map<Subscription>(request);
            subscription.TenantId = _currentTenant.TenantId!.Value;

            await _subscriptionRepository.AddAsync(subscription);

            CreatedSubscriptionResponse response = _mapper.Map<CreatedSubscriptionResponse>(subscription);
            return response;
        }
    }
}
