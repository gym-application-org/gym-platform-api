using Application.Features.WorkoutTemplates.Constants;
using Application.Features.WorkoutTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplates.Constants.WorkoutTemplatesOperationClaims;

namespace Application.Features.WorkoutTemplates.Queries.GetById;

public class GetByIdWorkoutTemplateQuery : IRequest<GetByIdWorkoutTemplateResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdWorkoutTemplateQueryHandler : IRequestHandler<GetByIdWorkoutTemplateQuery, GetByIdWorkoutTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly WorkoutTemplateBusinessRules _workoutTemplateBusinessRules;

        public GetByIdWorkoutTemplateQueryHandler(
            IMapper mapper,
            IWorkoutTemplateRepository workoutTemplateRepository,
            WorkoutTemplateBusinessRules workoutTemplateBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateRepository = workoutTemplateRepository;
            _workoutTemplateBusinessRules = workoutTemplateBusinessRules;
        }

        public async Task<GetByIdWorkoutTemplateResponse> Handle(GetByIdWorkoutTemplateQuery request, CancellationToken cancellationToken)
        {
            WorkoutTemplate? workoutTemplate = await _workoutTemplateRepository.GetAsync(
                predicate: wt => wt.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateBusinessRules.WorkoutTemplateShouldExistWhenSelected(workoutTemplate);

            GetByIdWorkoutTemplateResponse response = _mapper.Map<GetByIdWorkoutTemplateResponse>(workoutTemplate);
            return response;
        }
    }
}
