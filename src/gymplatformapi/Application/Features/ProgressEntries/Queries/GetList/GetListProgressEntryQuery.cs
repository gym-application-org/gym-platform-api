using Application.Features.ProgressEntries.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.ProgressEntries.Constants.ProgressEntriesOperationClaims;

namespace Application.Features.ProgressEntries.Queries.GetList;

public class GetListProgressEntryQuery : IRequest<GetListResponse<GetListProgressEntryListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListProgressEntries({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetProgressEntries";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListProgressEntryQueryHandler
        : IRequestHandler<GetListProgressEntryQuery, GetListResponse<GetListProgressEntryListItemDto>>
    {
        private readonly IProgressEntryRepository _progressEntryRepository;
        private readonly IMapper _mapper;

        public GetListProgressEntryQueryHandler(IProgressEntryRepository progressEntryRepository, IMapper mapper)
        {
            _progressEntryRepository = progressEntryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProgressEntryListItemDto>> Handle(
            GetListProgressEntryQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<ProgressEntry> progressEntries = await _progressEntryRepository.GetListAsync(
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
