using Application.Features.DietTemplateMeals.Constants;
using Application.Features.DietTemplateMeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMeals.Constants.DietTemplateMealsOperationClaims;

namespace Application.Features.DietTemplateMeals.Queries.GetById;

public class GetByIdDietTemplateMealQuery : IRequest<GetByIdDietTemplateMealResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdDietTemplateMealQueryHandler : IRequestHandler<GetByIdDietTemplateMealQuery, GetByIdDietTemplateMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateMealRepository _dietTemplateMealRepository;
        private readonly DietTemplateMealBusinessRules _dietTemplateMealBusinessRules;

        public GetByIdDietTemplateMealQueryHandler(
            IMapper mapper,
            IDietTemplateMealRepository dietTemplateMealRepository,
            DietTemplateMealBusinessRules dietTemplateMealBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateMealRepository = dietTemplateMealRepository;
            _dietTemplateMealBusinessRules = dietTemplateMealBusinessRules;
        }

        public async Task<GetByIdDietTemplateMealResponse> Handle(GetByIdDietTemplateMealQuery request, CancellationToken cancellationToken)
        {
            DietTemplateMeal? dietTemplateMeal = await _dietTemplateMealRepository.GetAsync(
                predicate: dtm => dtm.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateMealBusinessRules.DietTemplateMealShouldExistWhenSelected(dietTemplateMeal);

            GetByIdDietTemplateMealResponse response = _mapper.Map<GetByIdDietTemplateMealResponse>(dietTemplateMeal);
            return response;
        }
    }
}
