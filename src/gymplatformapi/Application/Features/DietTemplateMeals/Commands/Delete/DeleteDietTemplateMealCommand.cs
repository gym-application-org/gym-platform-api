using Application.Features.DietTemplateMeals.Constants;
using Application.Features.DietTemplateMeals.Constants;
using Application.Features.DietTemplateMeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMeals.Constants.DietTemplateMealsOperationClaims;

namespace Application.Features.DietTemplateMeals.Commands.Delete;

public class DeleteDietTemplateMealCommand
    : IRequest<DeletedDietTemplateMealResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateMealsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplateMeals"];

    public class DeleteDietTemplateMealCommandHandler : IRequestHandler<DeleteDietTemplateMealCommand, DeletedDietTemplateMealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateMealRepository _dietTemplateMealRepository;
        private readonly DietTemplateMealBusinessRules _dietTemplateMealBusinessRules;

        public DeleteDietTemplateMealCommandHandler(
            IMapper mapper,
            IDietTemplateMealRepository dietTemplateMealRepository,
            DietTemplateMealBusinessRules dietTemplateMealBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateMealRepository = dietTemplateMealRepository;
            _dietTemplateMealBusinessRules = dietTemplateMealBusinessRules;
        }

        public async Task<DeletedDietTemplateMealResponse> Handle(
            DeleteDietTemplateMealCommand request,
            CancellationToken cancellationToken
        )
        {
            DietTemplateMeal? dietTemplateMeal = await _dietTemplateMealRepository.GetAsync(
                predicate: dtm => dtm.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateMealBusinessRules.DietTemplateMealShouldExistWhenSelected(dietTemplateMeal);

            await _dietTemplateMealRepository.DeleteAsync(dietTemplateMeal!);

            DeletedDietTemplateMealResponse response = _mapper.Map<DeletedDietTemplateMealResponse>(dietTemplateMeal);
            return response;
        }
    }
}
