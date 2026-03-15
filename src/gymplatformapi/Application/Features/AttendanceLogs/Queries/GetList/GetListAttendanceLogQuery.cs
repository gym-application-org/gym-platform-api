using Application.Features.AttendanceLogs.Constants;
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
using static Application.Features.AttendanceLogs.Constants.AttendanceLogsOperationClaims;

namespace Application.Features.AttendanceLogs.Queries.GetList;

public class GetListAttendanceLogQuery : IRequest<GetListResponse<GetListAttendanceLogListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }
    public int? GateId { get; set; }
    public AttendanceResult? Result { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetListAttendanceLogQueryHandler
        : IRequestHandler<GetListAttendanceLogQuery, GetListResponse<GetListAttendanceLogListItemDto>>
    {
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IMapper _mapper;

        public GetListAttendanceLogQueryHandler(IAttendanceLogRepository attendanceLogRepository, IMapper mapper)
        {
            _attendanceLogRepository = attendanceLogRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAttendanceLogListItemDto>> Handle(
            GetListAttendanceLogQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<AttendanceLog> attendanceLogs = await _attendanceLogRepository.GetListAsync(
                predicate: x =>
                    (!request.GateId.HasValue || x.GateId == request.GateId)
                    && (!request.Result.HasValue || x.Result == request.Result)
                    && (!request.From.HasValue || x.CreatedDate >= request.From)
                    && (!request.To.HasValue || x.CreatedDate <= request.To),
                orderBy: q => q.OrderByDescending(x => x.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAttendanceLogListItemDto> response = _mapper.Map<GetListResponse<GetListAttendanceLogListItemDto>>(
                attendanceLogs
            );
            return response;
        }
    }
}
