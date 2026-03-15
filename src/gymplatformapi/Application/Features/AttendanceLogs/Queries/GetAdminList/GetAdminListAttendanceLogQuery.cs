using System;
using System.Linq;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.AttendanceLogs.Queries.GetAdminList;

public class GetAdminListAttendanceLogQuery : IRequest<GetListResponse<GetAdminListAttendanceLogListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; } = default!;
    public Guid? TenantId { get; set; }
    public Guid? MemberId { get; set; }
    public int? GateId { get; set; }
    public AttendanceResult? Result { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class GetAdminListAttendanceLogQueryHandler
        : IRequestHandler<GetAdminListAttendanceLogQuery, GetListResponse<GetAdminListAttendanceLogListItemDto>>
    {
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IMapper _mapper;

        public GetAdminListAttendanceLogQueryHandler(IAttendanceLogRepository attendanceLogRepository, IMapper mapper)
        {
            _attendanceLogRepository = attendanceLogRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetAdminListAttendanceLogListItemDto>> Handle(
            GetAdminListAttendanceLogQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<AttendanceLog> attendanceLogs = await _attendanceLogRepository.GetListAsync(
                predicate: x =>
                    (!request.TenantId.HasValue || x.TenantId == request.TenantId)
                    && (!request.MemberId.HasValue || x.MemberId == request.MemberId)
                    && (!request.GateId.HasValue || x.GateId == request.GateId)
                    && (!request.Result.HasValue || x.Result == request.Result)
                    && (!request.From.HasValue || x.CreatedDate >= request.From)
                    && (!request.To.HasValue || x.CreatedDate <= request.To),
                orderBy: q => q.OrderByDescending(x => x.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                withDeleted: true,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetAdminListAttendanceLogListItemDto> response = _mapper.Map<
                GetListResponse<GetAdminListAttendanceLogListItemDto>
            >(attendanceLogs);

            return response;
        }
    }
}
