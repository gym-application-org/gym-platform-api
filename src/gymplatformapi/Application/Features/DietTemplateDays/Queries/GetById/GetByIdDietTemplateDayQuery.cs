using Application.Features.DietTemplateDays.Constants;
using Application.Features.DietTemplateDays.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateDays.Constants.DietTemplateDaysOperationClaims;

namespace Application.Features.DietTemplateDays.Queries.GetById;

public class GetByIdDietTemplateDayQuery : IRequest<GetByIdDietTemplateDayResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdDietTemplateDayQueryHandler : IRequestHandler<GetByIdDietTemplateDayQuery, GetByIdDietTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateDayRepository _dietTemplateDayRepository;
        private readonly DietTemplateDayBusinessRules _dietTemplateDayBusinessRules;

        public GetByIdDietTemplateDayQueryHandler(
            IMapper mapper,
            IDietTemplateDayRepository dietTemplateDayRepository,
            DietTemplateDayBusinessRules dietTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateDayRepository = dietTemplateDayRepository;
            _dietTemplateDayBusinessRules = dietTemplateDayBusinessRules;
        }

        public async Task<GetByIdDietTemplateDayResponse> Handle(GetByIdDietTemplateDayQuery request, CancellationToken cancellationToken)
        {
            DietTemplateDay? dietTemplateDay = await _dietTemplateDayRepository.GetAsync(
                predicate: dtd => dtd.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateDayBusinessRules.DietTemplateDayShouldExistWhenSelected(dietTemplateDay);

            GetByIdDietTemplateDayResponse response = _mapper.Map<GetByIdDietTemplateDayResponse>(dietTemplateDay);
            return response;
        }
    }
}
