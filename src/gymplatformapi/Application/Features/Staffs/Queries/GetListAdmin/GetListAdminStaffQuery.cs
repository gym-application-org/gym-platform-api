using Application.Features.Staffs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Queries.GetListAdmin;

public class GetListAdminStaffQuery : IRequest<GetListResponse<GetListAdminStaffListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public Guid? TenantId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class GetListStaffQueryHandler : IRequestHandler<GetListAdminStaffQuery, GetListResponse<GetListAdminStaffListItemDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetListStaffQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAdminStaffListItemDto>> Handle(
            GetListAdminStaffQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Staff> staffs = await _staffRepository.GetListAsync(
                predicate: x => (!request.TenantId.HasValue || request.TenantId == x.TenantId),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: true,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAdminStaffListItemDto> response = _mapper.Map<GetListResponse<GetListAdminStaffListItemDto>>(staffs);
            return response;
        }
    }
}
