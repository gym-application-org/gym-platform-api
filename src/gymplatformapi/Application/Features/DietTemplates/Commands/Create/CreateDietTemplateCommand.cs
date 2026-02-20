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

namespace Application.Features.DietTemplates.Commands.Create;

public class CreateDietTemplateCommand
    : IRequest<CreatedDietTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? CaloriesTarget { get; set; }
    public int? ProteinGramsTarget { get; set; }
    public int? CarbGramsTarget { get; set; }
    public int? FatGramsTarget { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, DietTemplatesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplates"];

    public class CreateDietTemplateCommandHandler : IRequestHandler<CreateDietTemplateCommand, CreatedDietTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateRepository _dietTemplateRepository;
        private readonly DietTemplateBusinessRules _dietTemplateBusinessRules;

        public CreateDietTemplateCommandHandler(
            IMapper mapper,
            IDietTemplateRepository dietTemplateRepository,
            DietTemplateBusinessRules dietTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateRepository = dietTemplateRepository;
            _dietTemplateBusinessRules = dietTemplateBusinessRules;
        }

        public async Task<CreatedDietTemplateResponse> Handle(CreateDietTemplateCommand request, CancellationToken cancellationToken)
        {
            DietTemplate dietTemplate = _mapper.Map<DietTemplate>(request);

            await _dietTemplateRepository.AddAsync(dietTemplate);

            CreatedDietTemplateResponse response = _mapper.Map<CreatedDietTemplateResponse>(dietTemplate);
            return response;
        }
    }
}
