using Application.Features.DietTemplates.Commands.Create.Dtos;
using Application.Features.DietTemplates.Commands.Update.Dtos;
using Application.Features.DietTemplates.Constants;
using Application.Features.DietTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.DietTemplates.Constants.DietTemplatesOperationClaims;

namespace Application.Features.DietTemplates.Commands.Update;

public class UpdateDietTemplateCommand
    : IRequest<UpdatedDietTemplateResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? CaloriesTarget { get; set; }
    public int? ProteinGramsTarget { get; set; }
    public int? CarbGramsTarget { get; set; }
    public int? FatGramsTarget { get; set; }
    public bool IsActive { get; set; }

    public ICollection<UpdateDietTemplateDayDto> Days { get; set; } = new List<UpdateDietTemplateDayDto>();

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class UpdateDietTemplateCommandHandler : IRequestHandler<UpdateDietTemplateCommand, UpdatedDietTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateRepository _dietTemplateRepository;
        private readonly DietTemplateBusinessRules _dietTemplateBusinessRules;
        private readonly ICurrentTenant _currentTenant;

        public UpdateDietTemplateCommandHandler(
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

        public async Task<UpdatedDietTemplateResponse> Handle(UpdateDietTemplateCommand request, CancellationToken cancellationToken)
        {
            Guid tenantId = _currentTenant.TenantId!.Value;

            DietTemplate? dietTemplate = await _dietTemplateRepository.GetAsync(
                predicate: x => x.Id == request.Id && x.TenantId == tenantId,
                include: q => q.Include(x => x.Days).ThenInclude(d => d.Meals).ThenInclude(m => m.Items),
                enableTracking: true,
                cancellationToken: cancellationToken
            );

            await _dietTemplateBusinessRules.DietTemplateShouldExistWhenSelected(dietTemplate);

            dietTemplate!.Name = request.Name;
            dietTemplate.Description = request.Description;
            dietTemplate.CaloriesTarget = request.CaloriesTarget;
            dietTemplate.ProteinGramsTarget = request.ProteinGramsTarget;
            dietTemplate.CarbGramsTarget = request.CarbGramsTarget;
            dietTemplate.FatGramsTarget = request.FatGramsTarget;
            dietTemplate.IsActive = request.IsActive;

            dietTemplate.Days.Clear();

            foreach (var dayDto in request.Days)
            {
                DietTemplateDay day =
                    new(tenantId: tenantId, dietTemplateId: dietTemplate.Id, dayNumber: dayDto.DayNumber, title: dayDto.Title)
                    {
                        Id = dayDto.Id,
                        Notes = dayDto.Notes
                    };

                foreach (var mealDto in dayDto.Meals)
                {
                    DietTemplateMeal meal =
                        new(tenantId: tenantId, dietTemplateDayId: 0, name: mealDto.Name, order: mealDto.Order, notes: mealDto.Notes)
                        {
                            Id = mealDto.Id,
                        };

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
                            )
                            {
                                Id = itemDto.Id,
                            };

                        meal.Items.Add(item);
                    }

                    day.Meals.Add(meal);
                }

                dietTemplate.Days.Add(day);
            }

            await _dietTemplateRepository.UpdateAsync(dietTemplate!);

            UpdatedDietTemplateResponse response = _mapper.Map<UpdatedDietTemplateResponse>(dietTemplate);
            return response;
        }
    }
}
