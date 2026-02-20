using Application.Features.DietTemplates.Constants;
using Application.Features.DietTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplates.Constants.DietTemplatesOperationClaims;

namespace Application.Features.DietTemplates.Queries.GetById;

public class GetByIdDietTemplateQuery : IRequest<GetByIdDietTemplateResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdDietTemplateQueryHandler : IRequestHandler<GetByIdDietTemplateQuery, GetByIdDietTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateRepository _dietTemplateRepository;
        private readonly DietTemplateBusinessRules _dietTemplateBusinessRules;

        public GetByIdDietTemplateQueryHandler(
            IMapper mapper,
            IDietTemplateRepository dietTemplateRepository,
            DietTemplateBusinessRules dietTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateRepository = dietTemplateRepository;
            _dietTemplateBusinessRules = dietTemplateBusinessRules;
        }

        public async Task<GetByIdDietTemplateResponse> Handle(GetByIdDietTemplateQuery request, CancellationToken cancellationToken)
        {
            DietTemplate? dietTemplate = await _dietTemplateRepository.GetAsync(
                predicate: dt => dt.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateBusinessRules.DietTemplateShouldExistWhenSelected(dietTemplate);

            GetByIdDietTemplateResponse response = _mapper.Map<GetByIdDietTemplateResponse>(dietTemplate);
            return response;
        }
    }
}
