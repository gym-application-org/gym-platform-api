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

namespace Application.Features.DietTemplateDays.Commands.Create;

public class CreateDietTemplateDayCommand
    : IRequest<CreatedDietTemplateDayResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateId { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateDaysOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplateDays"];

    public class CreateDietTemplateDayCommandHandler : IRequestHandler<CreateDietTemplateDayCommand, CreatedDietTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateDayRepository _dietTemplateDayRepository;
        private readonly DietTemplateDayBusinessRules _dietTemplateDayBusinessRules;

        public CreateDietTemplateDayCommandHandler(
            IMapper mapper,
            IDietTemplateDayRepository dietTemplateDayRepository,
            DietTemplateDayBusinessRules dietTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateDayRepository = dietTemplateDayRepository;
            _dietTemplateDayBusinessRules = dietTemplateDayBusinessRules;
        }

        public async Task<CreatedDietTemplateDayResponse> Handle(CreateDietTemplateDayCommand request, CancellationToken cancellationToken)
        {
            DietTemplateDay dietTemplateDay = _mapper.Map<DietTemplateDay>(request);

            await _dietTemplateDayRepository.AddAsync(dietTemplateDay);

            CreatedDietTemplateDayResponse response = _mapper.Map<CreatedDietTemplateDayResponse>(dietTemplateDay);
            return response;
        }
    }
}
