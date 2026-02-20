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

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreateWorkoutTemplateCommand
    : IRequest<CreatedWorkoutTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplatesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutTemplates"];

    public class CreateWorkoutTemplateCommandHandler : IRequestHandler<CreateWorkoutTemplateCommand, CreatedWorkoutTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly WorkoutTemplateBusinessRules _workoutTemplateBusinessRules;

        public CreateWorkoutTemplateCommandHandler(
            IMapper mapper,
            IWorkoutTemplateRepository workoutTemplateRepository,
            WorkoutTemplateBusinessRules workoutTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateRepository = workoutTemplateRepository;
            _workoutTemplateBusinessRules = workoutTemplateBusinessRules;
        }

        public async Task<CreatedWorkoutTemplateResponse> Handle(CreateWorkoutTemplateCommand request, CancellationToken cancellationToken)
        {
            WorkoutTemplate workoutTemplate = _mapper.Map<WorkoutTemplate>(request);

            await _workoutTemplateRepository.AddAsync(workoutTemplate);

            CreatedWorkoutTemplateResponse response = _mapper.Map<CreatedWorkoutTemplateResponse>(workoutTemplate);
            return response;
        }
    }
}
