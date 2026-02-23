using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Subscriptions.Queries.GetById;
using Application.Features.Subscriptions.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Subscriptions.Queries.GetMy;

public class GetMyByIdSubscriptionQuery : IRequest<GetMyByIdSubscriptionResponse>, ISecuredRequest, ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class GetMyByIdSubscriptionQueryHandler : IRequestHandler<GetMyByIdSubscriptionQuery, GetMyByIdSubscriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly SubscriptionBusinessRules _subscriptionBusinessRules;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;

        public GetMyByIdSubscriptionQueryHandler(
            IMapper mapper,
            ISubscriptionRepository subscriptionRepository,
            SubscriptionBusinessRules subscriptionBusinessRules,
            ICurrentUser currentUser,
            IMemberService memberService
        )
        {
            _mapper = mapper;
            _subscriptionRepository = subscriptionRepository;
            _subscriptionBusinessRules = subscriptionBusinessRules;
            _currentUser = currentUser;
            _memberService = memberService;
        }

        public async Task<GetMyByIdSubscriptionResponse> Handle(GetMyByIdSubscriptionQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(x => x.UserId == _currentUser.UserId, cancellationToken: cancellationToken);
            await _subscriptionBusinessRules.MemberShouldExistWhenSelected(member);

            Subscription? subscription = await _subscriptionRepository.GetAsync(
                predicate: s => s.Id == request.Id && s.MemberId == member!.Id,
                cancellationToken: cancellationToken
            );
            await _subscriptionBusinessRules.SubscriptionShouldExistWhenSelected(subscription);

            GetMyByIdSubscriptionResponse response = _mapper.Map<GetMyByIdSubscriptionResponse>(subscription);
            return response;
        }
    }
}
