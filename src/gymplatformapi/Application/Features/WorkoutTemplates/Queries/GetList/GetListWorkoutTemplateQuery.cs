using Application.Features.WorkoutTemplates.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.WorkoutTemplates.Constants.WorkoutTemplatesOperationClaims;

namespace Application.Features.WorkoutTemplates.Queries.GetList;

public class GetListWorkoutTemplateQuery : IRequest<GetListResponse<GetListWorkoutTemplateListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetListWorkoutTemplateQueryHandler
        : IRequestHandler<GetListWorkoutTemplateQuery, GetListResponse<GetListWorkoutTemplateListItemDto>>
    {
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly IMapper _mapper;

        public GetListWorkoutTemplateQueryHandler(IWorkoutTemplateRepository workoutTemplateRepository, IMapper mapper)
        {
            _workoutTemplateRepository = workoutTemplateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWorkoutTemplateListItemDto>> Handle(
            GetListWorkoutTemplateQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<WorkoutTemplate> workoutTemplates = await _workoutTemplateRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: q => q.Include(q => q.Days),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWorkoutTemplateListItemDto> response = _mapper.Map<GetListResponse<GetListWorkoutTemplateListItemDto>>(
                workoutTemplates
            );
            return response;
        }
    }
}
