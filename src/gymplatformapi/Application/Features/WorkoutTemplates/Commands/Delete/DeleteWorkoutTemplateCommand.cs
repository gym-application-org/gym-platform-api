using Application.Features.WorkoutTemplates.Constants;
using Application.Features.WorkoutTemplates.Constants;
using Application.Features.WorkoutTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplates.Constants.WorkoutTemplatesOperationClaims;

namespace Application.Features.WorkoutTemplates.Commands.Delete;

public class DeleteWorkoutTemplateCommand
    : IRequest<DeletedWorkoutTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplatesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutTemplates"];

    public class DeleteWorkoutTemplateCommandHandler : IRequestHandler<DeleteWorkoutTemplateCommand, DeletedWorkoutTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly WorkoutTemplateBusinessRules _workoutTemplateBusinessRules;

        public DeleteWorkoutTemplateCommandHandler(
            IMapper mapper,
            IWorkoutTemplateRepository workoutTemplateRepository,
            WorkoutTemplateBusinessRules workoutTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateRepository = workoutTemplateRepository;
            _workoutTemplateBusinessRules = workoutTemplateBusinessRules;
        }

        public async Task<DeletedWorkoutTemplateResponse> Handle(DeleteWorkoutTemplateCommand request, CancellationToken cancellationToken)
        {
            WorkoutTemplate? workoutTemplate = await _workoutTemplateRepository.GetAsync(
                predicate: wt => wt.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateBusinessRules.WorkoutTemplateShouldExistWhenSelected(workoutTemplate);

            await _workoutTemplateRepository.DeleteAsync(workoutTemplate!);

            DeletedWorkoutTemplateResponse response = _mapper.Map<DeletedWorkoutTemplateResponse>(workoutTemplate);
            return response;
        }
    }
}
