using Application.Features.DietTemplateMealItems.Constants;
using Application.Features.DietTemplateMealItems.Constants;
using Application.Features.DietTemplateMealItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMealItems.Constants.DietTemplateMealItemsOperationClaims;

namespace Application.Features.DietTemplateMealItems.Commands.Delete;

public class DeleteDietTemplateMealItemCommand
    : IRequest<DeletedDietTemplateMealItemResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateMealItemsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplateMealItems"];

    public class DeleteDietTemplateMealItemCommandHandler
        : IRequestHandler<DeleteDietTemplateMealItemCommand, DeletedDietTemplateMealItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateMealItemRepository _dietTemplateMealItemRepository;
        private readonly DietTemplateMealItemBusinessRules _dietTemplateMealItemBusinessRules;

        public DeleteDietTemplateMealItemCommandHandler(
            IMapper mapper,
            IDietTemplateMealItemRepository dietTemplateMealItemRepository,
            DietTemplateMealItemBusinessRules dietTemplateMealItemBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateMealItemRepository = dietTemplateMealItemRepository;
            _dietTemplateMealItemBusinessRules = dietTemplateMealItemBusinessRules;
        }

        public async Task<DeletedDietTemplateMealItemResponse> Handle(
            DeleteDietTemplateMealItemCommand request,
            CancellationToken cancellationToken
        )
        {
            DietTemplateMealItem? dietTemplateMealItem = await _dietTemplateMealItemRepository.GetAsync(
                predicate: dtmi => dtmi.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateMealItemBusinessRules.DietTemplateMealItemShouldExistWhenSelected(dietTemplateMealItem);

            await _dietTemplateMealItemRepository.DeleteAsync(dietTemplateMealItem!);

            DeletedDietTemplateMealItemResponse response = _mapper.Map<DeletedDietTemplateMealItemResponse>(dietTemplateMealItem);
            return response;
        }
    }
}
