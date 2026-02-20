using Application.Features.DietTemplateMeals.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMeals.Constants.DietTemplateMealsOperationClaims;

namespace Application.Features.DietTemplateMeals.Queries.GetList;

public class GetListDietTemplateMealQuery : IRequest<GetListResponse<GetListDietTemplateMealListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDietTemplateMeals({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDietTemplateMeals";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDietTemplateMealQueryHandler
        : IRequestHandler<GetListDietTemplateMealQuery, GetListResponse<GetListDietTemplateMealListItemDto>>
    {
        private readonly IDietTemplateMealRepository _dietTemplateMealRepository;
        private readonly IMapper _mapper;

        public GetListDietTemplateMealQueryHandler(IDietTemplateMealRepository dietTemplateMealRepository, IMapper mapper)
        {
            _dietTemplateMealRepository = dietTemplateMealRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDietTemplateMealListItemDto>> Handle(
            GetListDietTemplateMealQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<DietTemplateMeal> dietTemplateMeals = await _dietTemplateMealRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDietTemplateMealListItemDto> response = _mapper.Map<GetListResponse<GetListDietTemplateMealListItemDto>>(
                dietTemplateMeals
            );
            return response;
        }
    }
}
