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

namespace Application.Features.DietTemplateMeals.Commands.Update;

public class UpdateDietTemplateMealCommand
    : IRequest<UpdatedDietTemplateMealResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateDayId { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateMealsOperationClaims.Update];

    public class UpdateDietTemplateMealCommandHandler : IRequestHandler<UpdateDietTemplateMealCommand, UpdatedDietTemplateMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateMealRepository _dietTemplateMealRepository;
        private readonly DietTemplateMealBusinessRules _dietTemplateMealBusinessRules;

        public UpdateDietTemplateMealCommandHandler(
            IMapper mapper,
            IDietTemplateMealRepository dietTemplateMealRepository,
            DietTemplateMealBusinessRules dietTemplateMealBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateMealRepository = dietTemplateMealRepository;
            _dietTemplateMealBusinessRules = dietTemplateMealBusinessRules;
        }

        public async Task<UpdatedDietTemplateMealResponse> Handle(
            UpdateDietTemplateMealCommand request,
            CancellationToken cancellationToken
        )
        {
            DietTemplateMeal? dietTemplateMeal = await _dietTemplateMealRepository.GetAsync(
                predicate: dtm => dtm.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateMealBusinessRules.DietTemplateMealShouldExistWhenSelected(dietTemplateMeal);
            dietTemplateMeal = _mapper.Map(request, dietTemplateMeal);

            await _dietTemplateMealRepository.UpdateAsync(dietTemplateMeal!);

            UpdatedDietTemplateMealResponse response = _mapper.Map<UpdatedDietTemplateMealResponse>(dietTemplateMeal);
            return response;
        }
    }
}
