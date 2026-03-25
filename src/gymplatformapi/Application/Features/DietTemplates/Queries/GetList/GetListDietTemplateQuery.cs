using Application.Features.DietTemplates.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.DietTemplates.Constants.DietTemplatesOperationClaims;

namespace Application.Features.DietTemplates.Queries.GetList;

public class GetListDietTemplateQuery : IRequest<GetListResponse<GetListDietTemplateListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetListDietTemplateQueryHandler
        : IRequestHandler<GetListDietTemplateQuery, GetListResponse<GetListDietTemplateListItemDto>>
    {
        private readonly IDietTemplateRepository _dietTemplateRepository;
        private readonly IMapper _mapper;

        public GetListDietTemplateQueryHandler(IDietTemplateRepository dietTemplateRepository, IMapper mapper)
        {
            _dietTemplateRepository = dietTemplateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDietTemplateListItemDto>> Handle(
            GetListDietTemplateQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<DietTemplate> dietTemplates = await _dietTemplateRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: dt => dt.Include(d => d.Days),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDietTemplateListItemDto> response = _mapper.Map<GetListResponse<GetListDietTemplateListItemDto>>(
                dietTemplates
            );
            return response;
        }
    }
}
