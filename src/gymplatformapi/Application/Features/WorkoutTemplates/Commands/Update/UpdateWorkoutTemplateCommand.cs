using Application.Features.WorkoutTemplates.Constants;
using Application.Features.WorkoutTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.WorkoutTemplates.Constants.WorkoutTemplatesOperationClaims;

namespace Application.Features.WorkoutTemplates.Commands.Update;

public class UpdateWorkoutTemplateCommand
    : IRequest<UpdatedWorkoutTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplatesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutTemplates"];

    public class UpdateWorkoutTemplateCommandHandler : IRequestHandler<UpdateWorkoutTemplateCommand, UpdatedWorkoutTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly WorkoutTemplateBusinessRules _workoutTemplateBusinessRules;

        public UpdateWorkoutTemplateCommandHandler(
            IMapper mapper,
            IWorkoutTemplateRepository workoutTemplateRepository,
            WorkoutTemplateBusinessRules workoutTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateRepository = workoutTemplateRepository;
            _workoutTemplateBusinessRules = workoutTemplateBusinessRules;
        }

        public async Task<UpdatedWorkoutTemplateResponse> Handle(UpdateWorkoutTemplateCommand request, CancellationToken cancellationToken)
        {
            WorkoutTemplate? workoutTemplate = await _workoutTemplateRepository.GetAsync(
                predicate: wt => wt.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateBusinessRules.WorkoutTemplateShouldExistWhenSelected(workoutTemplate);
            workoutTemplate = _mapper.Map(request, workoutTemplate);

            await _workoutTemplateRepository.UpdateAsync(workoutTemplate!);

            UpdatedWorkoutTemplateResponse response = _mapper.Map<UpdatedWorkoutTemplateResponse>(workoutTemplate);
            return response;
        }
    }
}
