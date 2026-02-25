using Application.Features.DietTemplates.Constants;
using Application.Features.DietTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.DietTemplates.Constants.DietTemplatesOperationClaims;

namespace Application.Features.DietTemplates.Queries.GetById;

public class GetByIdDietTemplateQuery : IRequest<GetByIdDietTemplateResponse>, ISecuredRequest, ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

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
                include: q => q.Include(x => x.Days).ThenInclude(d => d.Meals).ThenInclude(m => m.Items),
                enableTracking: true,
                cancellationToken: cancellationToken
            );
            await _dietTemplateBusinessRules.DietTemplateShouldExistWhenSelected(dietTemplate);

            GetByIdDietTemplateResponse response = _mapper.Map<GetByIdDietTemplateResponse>(dietTemplate);
            return response;
        }
    }
}
