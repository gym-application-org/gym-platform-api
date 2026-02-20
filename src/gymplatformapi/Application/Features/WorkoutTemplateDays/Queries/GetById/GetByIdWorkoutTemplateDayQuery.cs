using Application.Features.WorkoutTemplateDays.Constants;
using Application.Features.WorkoutTemplateDays.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDays.Constants.WorkoutTemplateDaysOperationClaims;

namespace Application.Features.WorkoutTemplateDays.Queries.GetById;

public class GetByIdWorkoutTemplateDayQuery : IRequest<GetByIdWorkoutTemplateDayResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdWorkoutTemplateDayQueryHandler : IRequestHandler<GetByIdWorkoutTemplateDayQuery, GetByIdWorkoutTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayRepository _workoutTemplateDayRepository;
        private readonly WorkoutTemplateDayBusinessRules _workoutTemplateDayBusinessRules;

        public GetByIdWorkoutTemplateDayQueryHandler(
            IMapper mapper,
            IWorkoutTemplateDayRepository workoutTemplateDayRepository,
            WorkoutTemplateDayBusinessRules workoutTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayRepository = workoutTemplateDayRepository;
            _workoutTemplateDayBusinessRules = workoutTemplateDayBusinessRules;
        }

        public async Task<GetByIdWorkoutTemplateDayResponse> Handle(
            GetByIdWorkoutTemplateDayQuery request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDay? workoutTemplateDay = await _workoutTemplateDayRepository.GetAsync(
                predicate: wtd => wtd.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateDayBusinessRules.WorkoutTemplateDayShouldExistWhenSelected(workoutTemplateDay);

            GetByIdWorkoutTemplateDayResponse response = _mapper.Map<GetByIdWorkoutTemplateDayResponse>(workoutTemplateDay);
            return response;
        }
    }
}
