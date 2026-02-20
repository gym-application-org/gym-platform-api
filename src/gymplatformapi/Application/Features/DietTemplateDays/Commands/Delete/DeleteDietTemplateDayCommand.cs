using Application.Features.DietTemplateDays.Constants;
using Application.Features.DietTemplateDays.Constants;
using Application.Features.DietTemplateDays.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.DietTemplateDays.Constants.DietTemplateDaysOperationClaims;

namespace Application.Features.DietTemplateDays.Commands.Delete;

public class DeleteDietTemplateDayCommand
    : IRequest<DeletedDietTemplateDayResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateDaysOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplateDays"];

    public class DeleteDietTemplateDayCommandHandler : IRequestHandler<DeleteDietTemplateDayCommand, DeletedDietTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateDayRepository _dietTemplateDayRepository;
        private readonly DietTemplateDayBusinessRules _dietTemplateDayBusinessRules;

        public DeleteDietTemplateDayCommandHandler(
            IMapper mapper,
            IDietTemplateDayRepository dietTemplateDayRepository,
            DietTemplateDayBusinessRules dietTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateDayRepository = dietTemplateDayRepository;
            _dietTemplateDayBusinessRules = dietTemplateDayBusinessRules;
        }

        public async Task<DeletedDietTemplateDayResponse> Handle(DeleteDietTemplateDayCommand request, CancellationToken cancellationToken)
        {
            DietTemplateDay? dietTemplateDay = await _dietTemplateDayRepository.GetAsync(
                predicate: dtd => dtd.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateDayBusinessRules.DietTemplateDayShouldExistWhenSelected(dietTemplateDay);

            await _dietTemplateDayRepository.DeleteAsync(dietTemplateDay!);

            DeletedDietTemplateDayResponse response = _mapper.Map<DeletedDietTemplateDayResponse>(dietTemplateDay);
            return response;
        }
    }
}
