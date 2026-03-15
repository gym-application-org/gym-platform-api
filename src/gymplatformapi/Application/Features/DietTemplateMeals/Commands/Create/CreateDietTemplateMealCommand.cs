using Application.Features.DietTemplateMeals.Constants;
using Application.Features.DietTemplateMeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMeals.Constants.DietTemplateMealsOperationClaims;

namespace Application.Features.DietTemplateMeals.Commands.Create;

public class CreateDietTemplateMealCommand
    : IRequest<CreatedDietTemplateMealResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string Name { get; set; }
    public int Order { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateDayId { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateMealsOperationClaims.Create];

    public class CreateDietTemplateMealCommandHandler : IRequestHandler<CreateDietTemplateMealCommand, CreatedDietTemplateMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateMealRepository _dietTemplateMealRepository;
        private readonly DietTemplateMealBusinessRules _dietTemplateMealBusinessRules;

        public CreateDietTemplateMealCommandHandler(
            IMapper mapper,
            IDietTemplateMealRepository dietTemplateMealRepository,
            DietTemplateMealBusinessRules dietTemplateMealBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateMealRepository = dietTemplateMealRepository;
            _dietTemplateMealBusinessRules = dietTemplateMealBusinessRules;
        }

        public async Task<CreatedDietTemplateMealResponse> Handle(
            CreateDietTemplateMealCommand request,
            CancellationToken cancellationToken
        )
        {
            DietTemplateMeal dietTemplateMeal = _mapper.Map<DietTemplateMeal>(request);

            await _dietTemplateMealRepository.AddAsync(dietTemplateMeal);

            CreatedDietTemplateMealResponse response = _mapper.Map<CreatedDietTemplateMealResponse>(dietTemplateMeal);
            return response;
        }
    }
}
