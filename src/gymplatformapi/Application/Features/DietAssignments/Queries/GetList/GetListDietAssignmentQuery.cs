using Application.Features.DietAssignments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Queries.GetList;

public class GetListDietAssignmentQuery : IRequest<GetListResponse<GetListDietAssignmentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDietAssignments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDietAssignments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDietAssignmentQueryHandler
        : IRequestHandler<GetListDietAssignmentQuery, GetListResponse<GetListDietAssignmentListItemDto>>
    {
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly IMapper _mapper;

        public GetListDietAssignmentQueryHandler(IDietAssignmentRepository dietAssignmentRepository, IMapper mapper)
        {
            _dietAssignmentRepository = dietAssignmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDietAssignmentListItemDto>> Handle(
            GetListDietAssignmentQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<DietAssignment> dietAssignments = await _dietAssignmentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDietAssignmentListItemDto> response = _mapper.Map<GetListResponse<GetListDietAssignmentListItemDto>>(
                dietAssignments
            );
            return response;
        }
    }
}
