using Application.Features.DietTemplates.Constants;
using Application.Features.DietTemplates.Constants;
using Application.Features.DietTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplates.Constants.DietTemplatesOperationClaims;

namespace Application.Features.DietTemplates.Commands.Delete;

public class DeleteDietTemplateCommand
    : IRequest<DeletedDietTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplates"];

    public class DeleteDietTemplateCommandHandler : IRequestHandler<DeleteDietTemplateCommand, DeletedDietTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateRepository _dietTemplateRepository;
        private readonly DietTemplateBusinessRules _dietTemplateBusinessRules;

        public DeleteDietTemplateCommandHandler(
            IMapper mapper,
            IDietTemplateRepository dietTemplateRepository,
            DietTemplateBusinessRules dietTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateRepository = dietTemplateRepository;
            _dietTemplateBusinessRules = dietTemplateBusinessRules;
        }

        public async Task<DeletedDietTemplateResponse> Handle(DeleteDietTemplateCommand request, CancellationToken cancellationToken)
        {
            DietTemplate? dietTemplate = await _dietTemplateRepository.GetAsync(
                predicate: dt => dt.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateBusinessRules.DietTemplateShouldExistWhenSelected(dietTemplate);

            await _dietTemplateRepository.DeleteAsync(dietTemplate!);

            DeletedDietTemplateResponse response = _mapper.Map<DeletedDietTemplateResponse>(dietTemplate);
            return response;
        }
    }
}
