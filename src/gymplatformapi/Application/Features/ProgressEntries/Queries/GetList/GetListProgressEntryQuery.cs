using Application.Features.ProgressEntries.Constants;
using Application.Features.ProgressEntries.Rules;
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
using static Application.Features.ProgressEntries.Constants.ProgressEntriesOperationClaims;

namespace Application.Features.ProgressEntries.Queries.GetList;

public class GetListProgressEntryQuery : IRequest<GetListResponse<GetListProgressEntryListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class GetListProgressEntryQueryHandler
        : IRequestHandler<GetListProgressEntryQuery, GetListResponse<GetListProgressEntryListItemDto>>
    {
        private readonly IProgressEntryRepository _progressEntryRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;
        private readonly ProgressEntryBusinessRules _progressEntryBusinessRules;

        public GetListProgressEntryQueryHandler(
            IProgressEntryRepository progressEntryRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IMemberService memberService,
            ProgressEntryBusinessRules progressEntryBusinessRules
        )
        {
            _progressEntryRepository = progressEntryRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _memberService = memberService;
            _progressEntryBusinessRules = progressEntryBusinessRules;
        }

        public async Task<GetListResponse<GetListProgressEntryListItemDto>> Handle(
            GetListProgressEntryQuery request,
            CancellationToken cancellationToken
        )
        {
            Member? member = await _memberService.GetAsync(
                predicate: x => x.UserId == _currentUser.UserId,
                cancellationToken: cancellationToken
            );
            await _progressEntryBusinessRules.MemberShouldExistWhenSelected(member);

            IPaginate<ProgressEntry> progressEntries = await _progressEntryRepository.GetListAsync(
                predicate: x =>
                    x.MemberId == member!.Id
                    && (!request.From.HasValue || x.Date >= request.From.Value)
                    && (!request.To.HasValue || x.Date <= request.To.Value),
                orderBy: x => x.OrderByDescending(y => y.Date),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProgressEntryListItemDto> response = _mapper.Map<GetListResponse<GetListProgressEntryListItemDto>>(
                progressEntries
            );
            return response;
        }
    }
}
