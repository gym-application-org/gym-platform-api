using Application.Features.DietTemplateMealItems.Constants;
using Application.Features.DietTemplateMealItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateMealItems.Constants.DietTemplateMealItemsOperationClaims;

namespace Application.Features.DietTemplateMealItems.Commands.Update;

public class UpdateDietTemplateMealItemCommand
    : IRequest<UpdatedDietTemplateMealItemResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public int Order { get; set; }
    public string FoodName { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public int? Calories { get; set; }
    public int? ProteinG { get; set; }
    public int? CarbG { get; set; }
    public int? FatG { get; set; }
    public string? Note { get; set; }
    public int DietTemplateMealId { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateMealItemsOperationClaims.Update];

    public class UpdateDietTemplateMealItemCommandHandler
        : IRequestHandler<UpdateDietTemplateMealItemCommand, UpdatedDietTemplateMealItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateMealItemRepository _dietTemplateMealItemRepository;
        private readonly DietTemplateMealItemBusinessRules _dietTemplateMealItemBusinessRules;

        public UpdateDietTemplateMealItemCommandHandler(
            IMapper mapper,
            IDietTemplateMealItemRepository dietTemplateMealItemRepository,
            DietTemplateMealItemBusinessRules dietTemplateMealItemBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateMealItemRepository = dietTemplateMealItemRepository;
            _dietTemplateMealItemBusinessRules = dietTemplateMealItemBusinessRules;
        }

        public async Task<UpdatedDietTemplateMealItemResponse> Handle(
            UpdateDietTemplateMealItemCommand request,
            CancellationToken cancellationToken
        )
        {
            DietTemplateMealItem? dietTemplateMealItem = await _dietTemplateMealItemRepository.GetAsync(
                predicate: dtmi => dtmi.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateMealItemBusinessRules.DietTemplateMealItemShouldExistWhenSelected(dietTemplateMealItem);
            dietTemplateMealItem = _mapper.Map(request, dietTemplateMealItem);

            await _dietTemplateMealItemRepository.UpdateAsync(dietTemplateMealItem!);

            UpdatedDietTemplateMealItemResponse response = _mapper.Map<UpdatedDietTemplateMealItemResponse>(dietTemplateMealItem);
            return response;
        }
    }
}
