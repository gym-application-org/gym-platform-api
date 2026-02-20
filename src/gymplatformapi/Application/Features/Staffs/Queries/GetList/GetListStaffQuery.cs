using Application.Features.Staffs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Queries.GetList;

public class GetListStaffQuery : IRequest<GetListResponse<GetListStaffListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListStaffs({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetStaffs";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStaffQueryHandler : IRequestHandler<GetListStaffQuery, GetListResponse<GetListStaffListItemDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetListStaffQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStaffListItemDto>> Handle(GetListStaffQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Staff> staffs = await _staffRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStaffListItemDto> response = _mapper.Map<GetListResponse<GetListStaffListItemDto>>(staffs);
            return response;
        }
    }
}
