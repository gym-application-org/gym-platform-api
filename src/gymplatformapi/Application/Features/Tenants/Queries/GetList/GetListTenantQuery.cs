using Application.Features.Tenants.Constants;
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
using static Application.Features.Tenants.Constants.TenantsOperationClaims;

namespace Application.Features.Tenants.Queries.GetList;

public class GetListTenantQuery : IRequest<GetListResponse<GetListTenantListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListTenants({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetTenants";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTenantQueryHandler : IRequestHandler<GetListTenantQuery, GetListResponse<GetListTenantListItemDto>>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;

        public GetListTenantQueryHandler(ITenantRepository tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTenantListItemDto>> Handle(GetListTenantQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Tenant> tenants = await _tenantRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: true,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTenantListItemDto> response = _mapper.Map<GetListResponse<GetListTenantListItemDto>>(tenants);
            return response;
        }
    }
}
