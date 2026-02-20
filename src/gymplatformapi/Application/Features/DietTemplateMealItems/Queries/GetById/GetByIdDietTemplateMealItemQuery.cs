using Application.Features.DietTemplateMealItems.Constants;
using Application.Features.DietTemplateMealItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMealItems.Constants.DietTemplateMealItemsOperationClaims;

namespace Application.Features.DietTemplateMealItems.Queries.GetById;

public class GetByIdDietTemplateMealItemQuery : IRequest<GetByIdDietTemplateMealItemResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdDietTemplateMealItemQueryHandler
        : IRequestHandler<GetByIdDietTemplateMealItemQuery, GetByIdDietTemplateMealItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateMealItemRepository _dietTemplateMealItemRepository;
        private readonly DietTemplateMealItemBusinessRules _dietTemplateMealItemBusinessRules;

        public GetByIdDietTemplateMealItemQueryHandler(
            IMapper mapper,
            IDietTemplateMealItemRepository dietTemplateMealItemRepository,
            DietTemplateMealItemBusinessRules dietTemplateMealItemBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateMealItemRepository = dietTemplateMealItemRepository;
            _dietTemplateMealItemBusinessRules = dietTemplateMealItemBusinessRules;
        }

        public async Task<GetByIdDietTemplateMealItemResponse> Handle(
            GetByIdDietTemplateMealItemQuery request,
            CancellationToken cancellationToken
        )
        {
            DietTemplateMealItem? dietTemplateMealItem = await _dietTemplateMealItemRepository.GetAsync(
                predicate: dtmi => dtmi.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateMealItemBusinessRules.DietTemplateMealItemShouldExistWhenSelected(dietTemplateMealItem);

            GetByIdDietTemplateMealItemResponse response = _mapper.Map<GetByIdDietTemplateMealItemResponse>(dietTemplateMealItem);
            return response;
        }
    }
}
