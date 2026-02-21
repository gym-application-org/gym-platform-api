using Application.Features.Gates.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Gates.Constants.GatesOperationClaims;

namespace Application.Features.Gates.Queries.GetListAdmin;

public class GetListGateQuery : IRequest<GetListResponse<GetListGateListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public Guid? TenantId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListGates({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetGates";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListGateQueryHandler : IRequestHandler<GetListGateQuery, GetListResponse<GetListGateListItemDto>>
    {
        private readonly IGateRepository _gateRepository;
        private readonly IMapper _mapper;

        public GetListGateQueryHandler(IGateRepository gateRepository, IMapper mapper)
        {
            _gateRepository = gateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListGateListItemDto>> Handle(GetListGateQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Gate> gates = await _gateRepository.GetListAsync(
                predicate: x => (!request.TenantId.HasValue || x.TenantId == request.TenantId),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: true,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListGateListItemDto> response = _mapper.Map<GetListResponse<GetListGateListItemDto>>(gates);
            return response;
        }
    }
}
