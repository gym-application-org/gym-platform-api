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

namespace Application.Features.AttendanceLogs.Queries.GetByMemberList;

public class GetByMemberListAttendanceLogQuery
    : IRequest<GetListResponse<GetByMemberListAttendanceLogListItemDto>>,
        ISecuredRequest,
        ITenantRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid MemberId { get; set; }
    public int? GateId { get; set; }
    public AttendanceResult? Result { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetByMemberListAttendanceLogQueryHandler
        : IRequestHandler<GetByMemberListAttendanceLogQuery, GetListResponse<GetByMemberListAttendanceLogListItemDto>>
    {
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IMapper _mapper;

        public GetByMemberListAttendanceLogQueryHandler(IAttendanceLogRepository attendanceLogRepository, IMapper mapper)
        {
            _attendanceLogRepository = attendanceLogRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetByMemberListAttendanceLogListItemDto>> Handle(
            GetByMemberListAttendanceLogQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<AttendanceLog> attendanceLogs = await _attendanceLogRepository.GetListAsync(
                predicate: x =>
                    x.MemberId == request.MemberId
                    && (!request.GateId.HasValue || x.GateId == request.GateId)
                    && (!request.Result.HasValue || x.Result == request.Result)
                    && (!request.From.HasValue || x.CreatedDate >= request.From)
                    && (!request.To.HasValue || x.CreatedDate <= request.To),
                orderBy: q => q.OrderByDescending(x => x.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetByMemberListAttendanceLogListItemDto> response = _mapper.Map<
                GetListResponse<GetByMemberListAttendanceLogListItemDto>
            >(attendanceLogs);
            return response;
        }
    }
}
