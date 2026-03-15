using Application.Features.Subscriptions.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Subscriptions.Constants.SubscriptionsOperationClaims;

namespace Application.Features.Subscriptions.Queries.GetMyList;

public class GetMyListSubscriptionQuery : IRequest<GetListResponse<GetMyListSubscriptionListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public int? SubscriptionPlanId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class GetMyListSubscriptionQueryHandler
        : IRequestHandler<GetMyListSubscriptionQuery, GetListResponse<GetMyListSubscriptionListItemDto>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;
        private readonly SubscriptionBusinessRules _subscriptionBusinessRules;

        public GetMyListSubscriptionQueryHandler(
            ISubscriptionRepository subscriptionRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IMemberService memberService,
            SubscriptionBusinessRules subscriptionBusinessRules
        )
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _memberService = memberService;
            _subscriptionBusinessRules = subscriptionBusinessRules;
        }

        public async Task<GetListResponse<GetMyListSubscriptionListItemDto>> Handle(
            GetMyListSubscriptionQuery request,
            CancellationToken cancellationToken
        )
        {
            Member? member = await _memberService.GetAsync(x => x.UserId == _currentUser.UserId, cancellationToken: cancellationToken);
            await _subscriptionBusinessRules.MemberShouldExistWhenSelected(member);

            IPaginate<Subscription> subscriptions = await _subscriptionRepository.GetListAsync(
                predicate: x =>
                    x.MemberId == member!.Id
                    && (!request.From.HasValue || x.StartDate >= request.From.Value)
                    && (!request.To.HasValue || x.EndDate <= request.To.Value)
                    && (!request.SubscriptionPlanId.HasValue || x.SubscriptionPlanId == request.SubscriptionPlanId.Value),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetMyListSubscriptionListItemDto> response = _mapper.Map<GetListResponse<GetMyListSubscriptionListItemDto>>(
                subscriptions
            );
            return response;
        }
    }
}
