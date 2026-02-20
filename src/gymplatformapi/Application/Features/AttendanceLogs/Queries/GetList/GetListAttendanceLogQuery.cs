using Application.Features.AttendanceLogs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.AttendanceLogs.Constants.AttendanceLogsOperationClaims;

namespace Application.Features.AttendanceLogs.Queries.GetList;

public class GetListAttendanceLogQuery : IRequest<GetListResponse<GetListAttendanceLogListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAttendanceLogs({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAttendanceLogs";
    public TimeSpan? SlidingExpiration { get; }

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
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAttendanceLogListItemDto> response = _mapper.Map<GetListResponse<GetListAttendanceLogListItemDto>>(
                attendanceLogs
            );
            return response;
        }
    }
}
