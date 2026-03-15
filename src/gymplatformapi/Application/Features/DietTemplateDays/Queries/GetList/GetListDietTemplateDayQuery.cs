using Application.Features.DietTemplateDays.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateDays.Constants.DietTemplateDaysOperationClaims;

namespace Application.Features.DietTemplateDays.Queries.GetList;

public class GetListDietTemplateDayQuery : IRequest<GetListResponse<GetListDietTemplateDayListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListDietTemplateDayQueryHandler
        : IRequestHandler<GetListDietTemplateDayQuery, GetListResponse<GetListDietTemplateDayListItemDto>>
    {
        private readonly IDietTemplateDayRepository _dietTemplateDayRepository;
        private readonly IMapper _mapper;

        public GetListDietTemplateDayQueryHandler(IDietTemplateDayRepository dietTemplateDayRepository, IMapper mapper)
        {
            _dietTemplateDayRepository = dietTemplateDayRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDietTemplateDayListItemDto>> Handle(
            GetListDietTemplateDayQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<DietTemplateDay> dietTemplateDays = await _dietTemplateDayRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDietTemplateDayListItemDto> response = _mapper.Map<GetListResponse<GetListDietTemplateDayListItemDto>>(
                dietTemplateDays
            );
            return response;
        }
    }
}
