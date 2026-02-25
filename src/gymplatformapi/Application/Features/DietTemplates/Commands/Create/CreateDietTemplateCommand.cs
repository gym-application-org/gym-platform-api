using Application.Features.DietTemplates.Commands.Create.Dtos;
using Application.Features.DietTemplates.Constants;
using Application.Features.DietTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplates.Constants.DietTemplatesOperationClaims;

namespace Application.Features.DietTemplates.Commands.Create;

public class CreateDietTemplateCommand
    : IRequest<CreatedDietTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? CaloriesTarget { get; set; }
    public int? ProteinGramsTarget { get; set; }
    public int? CarbGramsTarget { get; set; }
    public int? FatGramsTarget { get; set; }
    public bool IsActive { get; set; }

    public ICollection<CreateDietTemplateDayDto> Days { get; set; } = new List<CreateDietTemplateDayDto>();

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplates"];

    public class CreateDietTemplateCommandHandler : IRequestHandler<CreateDietTemplateCommand, CreatedDietTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateRepository _dietTemplateRepository;
        private readonly DietTemplateBusinessRules _dietTemplateBusinessRules;
        private readonly ICurrentTenant _currentTenant;

        public CreateDietTemplateCommandHandler(
            IMapper mapper,
            IDietTemplateRepository dietTemplateRepository,
            DietTemplateBusinessRules dietTemplateBusinessRules,
            ICurrentTenant currentTenant
        )
        {
            _mapper = mapper;
            _dietTemplateRepository = dietTemplateRepository;
            _dietTemplateBusinessRules = dietTemplateBusinessRules;
            _currentTenant = currentTenant;
        }

        public async Task<CreatedDietTemplateResponse> Handle(CreateDietTemplateCommand request, CancellationToken cancellationToken)
        {
            Guid tenantId = _currentTenant.TenantId!.Value;

            DietTemplate dietTemplate =
                new(
                    tenantId: tenantId,
                    name: request.Name,
                    description: request.Description,
                    caloriesTarget: request.CaloriesTarget,
                    proteinGramsTarget: request.ProteinGramsTarget,
                    carbGramsTarget: request.CarbGramsTarget,
                    fatGramsTarget: request.FatGramsTarget,
                    isActive: request.IsActive
                );

            foreach (var dayDto in request.Days)
            {
                DietTemplateDay day =
                    new(tenantId: tenantId, dietTemplateId: 0, dayNumber: dayDto.DayNumber, title: dayDto.Title) { Notes = dayDto.Notes };

                foreach (var mealDto in dayDto.Meals)
                {
                    DietTemplateMeal meal =
                        new(tenantId: tenantId, dietTemplateDayId: 0, name: mealDto.Name, order: mealDto.Order, notes: mealDto.Notes);

                    foreach (var itemDto in mealDto.Items)
                    {
                        DietTemplateMealItem item =
                            new(
                                tenantId: tenantId,
                                dietTemplateMealId: 0,
                                order: itemDto.Order,
                                foodName: itemDto.FoodName,
                                quantity: itemDto.Quantity,
                                unit: itemDto.Unit,
                                calories: itemDto.Calories,
                                proteinG: itemDto.ProteinG,
                                carbG: itemDto.CarbG,
                                fatG: itemDto.FatG,
                                note: itemDto.Note
                            );

                        meal.Items.Add(item);
                    }

                    day.Meals.Add(meal);
                }

                dietTemplate.Days.Add(day);
            }

            await _dietTemplateRepository.AddAsync(dietTemplate);

            CreatedDietTemplateResponse response = _mapper.Map<CreatedDietTemplateResponse>(dietTemplate);
            return response;
        }
    }
}
