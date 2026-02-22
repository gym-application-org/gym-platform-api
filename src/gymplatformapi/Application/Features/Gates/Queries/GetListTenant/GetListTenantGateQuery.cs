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

namespace Application.Features.Gates.Queries.GetListTenant;

public class GetListTenantGateQuery
    : IRequest<GetListResponse<GetListTenantGateListItemDto>>,
        ISecuredRequest,
        ICachableRequest,
        ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner, GeneralOperationClaims.Staff];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListGates({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetGates";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTenantGateQueryHandler : IRequestHandler<GetListTenantGateQuery, GetListResponse<GetListTenantGateListItemDto>>
    {
        private readonly IGateRepository _gateRepository;
        private readonly IMapper _mapper;

        public GetListTenantGateQueryHandler(IGateRepository gateRepository, IMapper mapper)
        {
            _gateRepository = gateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTenantGateListItemDto>> Handle(
            GetListTenantGateQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Gate> gates = await _gateRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTenantGateListItemDto> response = _mapper.Map<GetListResponse<GetListTenantGateListItemDto>>(gates);
            return response;
        }
    }
}
