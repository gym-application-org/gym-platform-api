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

namespace Application.Features.DietTemplateDays.Commands.Update;

public class UpdateDietTemplateDayCommand
    : IRequest<UpdatedDietTemplateDayResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateId { get; set; }

    public string[] Roles => [Admin, Write, DietTemplateDaysOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietTemplateDays"];

    public class UpdateDietTemplateDayCommandHandler : IRequestHandler<UpdateDietTemplateDayCommand, UpdatedDietTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietTemplateDayRepository _dietTemplateDayRepository;
        private readonly DietTemplateDayBusinessRules _dietTemplateDayBusinessRules;

        public UpdateDietTemplateDayCommandHandler(
            IMapper mapper,
            IDietTemplateDayRepository dietTemplateDayRepository,
            DietTemplateDayBusinessRules dietTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _dietTemplateDayRepository = dietTemplateDayRepository;
            _dietTemplateDayBusinessRules = dietTemplateDayBusinessRules;
        }

        public async Task<UpdatedDietTemplateDayResponse> Handle(UpdateDietTemplateDayCommand request, CancellationToken cancellationToken)
        {
            DietTemplateDay? dietTemplateDay = await _dietTemplateDayRepository.GetAsync(
                predicate: dtd => dtd.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietTemplateDayBusinessRules.DietTemplateDayShouldExistWhenSelected(dietTemplateDay);
            dietTemplateDay = _mapper.Map(request, dietTemplateDay);

            await _dietTemplateDayRepository.UpdateAsync(dietTemplateDay!);

            UpdatedDietTemplateDayResponse response = _mapper.Map<UpdatedDietTemplateDayResponse>(dietTemplateDay);
            return response;
        }
    }
}
