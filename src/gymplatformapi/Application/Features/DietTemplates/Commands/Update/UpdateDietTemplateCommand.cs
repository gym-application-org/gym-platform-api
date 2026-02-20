using Application.Features.DietTemplates.Constants;
using Application.Features.DietTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplates.Constants.DietTemplatesOperationClaims;

namespace Application.Features.DietTemplates.Commands.Update;

public class UpdateDietTemplateCommand
    : IRequest<UpdatedDietTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? CaloriesTarget { get; set; }
    public int? ProteinGramsTarget { get; set; }
    public int? CarbGramsTarget { get; set; }
    public int? FatGramsTarget { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, DietTemplatesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplates"];

    public class UpdateDietTemplateCommandHandler : IRequestHandler<UpdateDietTemplateCommand, UpdatedDietTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateRepository _dietTemplateRepository;
        private readonly DietTemplateBusinessRules _dietTemplateBusinessRules;

        public UpdateDietTemplateCommandHandler(
            IMapper mapper,
            IDietTemplateRepository dietTemplateRepository,
            DietTemplateBusinessRules dietTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateRepository = dietTemplateRepository;
            _dietTemplateBusinessRules = dietTemplateBusinessRules;
        }

        public async Task<UpdatedDietTemplateResponse> Handle(UpdateDietTemplateCommand request, CancellationToken cancellationToken)
        {
            DietTemplate? dietTemplate = await _dietTemplateRepository.GetAsync(
                predicate: dt => dt.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateBusinessRules.DietTemplateShouldExistWhenSelected(dietTemplate);
            dietTemplate = _mapper.Map(request, dietTemplate);

            await _dietTemplateRepository.UpdateAsync(dietTemplate!);

            UpdatedDietTemplateResponse response = _mapper.Map<UpdatedDietTemplateResponse>(dietTemplate);
            return response;
        }
    }
}
