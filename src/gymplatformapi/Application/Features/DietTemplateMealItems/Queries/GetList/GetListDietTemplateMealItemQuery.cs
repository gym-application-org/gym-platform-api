using Application.Features.DietTemplateMealItems.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMealItems.Constants.DietTemplateMealItemsOperationClaims;

namespace Application.Features.DietTemplateMealItems.Queries.GetList;

public class GetListDietTemplateMealItemQuery : IRequest<GetListResponse<GetListDietTemplateMealItemListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListDietTemplateMealItemQueryHandler
        : IRequestHandler<GetListDietTemplateMealItemQuery, GetListResponse<GetListDietTemplateMealItemListItemDto>>
    {
        private readonly IDietTemplateMealItemRepository _dietTemplateMealItemRepository;
        private readonly IMapper _mapper;

        public GetListDietTemplateMealItemQueryHandler(IDietTemplateMealItemRepository dietTemplateMealItemRepository, IMapper mapper)
        {
            _dietTemplateMealItemRepository = dietTemplateMealItemRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDietTemplateMealItemListItemDto>> Handle(
            GetListDietTemplateMealItemQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<DietTemplateMealItem> dietTemplateMealItems = await _dietTemplateMealItemRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDietTemplateMealItemListItemDto> response = _mapper.Map<
                GetListResponse<GetListDietTemplateMealItemListItemDto>
            >(dietTemplateMealItems);
            return response;
        }
    }
}
