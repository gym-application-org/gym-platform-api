using Application.Features.AttendanceLogs.Constants;
using Application.Features.AttendanceLogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.AttendanceLogs.Constants.AttendanceLogsOperationClaims;

namespace Application.Features.AttendanceLogs.Queries.GetById;

public class GetByIdAttendanceLogQuery : IRequest<GetByIdAttendanceLogResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdAttendanceLogQueryHandler : IRequestHandler<GetByIdAttendanceLogQuery, GetByIdAttendanceLogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly AttendanceLogBusinessRules _attendanceLogBusinessRules;

        public GetByIdAttendanceLogQueryHandler(
            IMapper mapper,
            IAttendanceLogRepository attendanceLogRepository,
            AttendanceLogBusinessRules attendanceLogBusinessRules
        )
        {
            _mapper = mapper;
            _attendanceLogRepository = attendanceLogRepository;
            _attendanceLogBusinessRules = attendanceLogBusinessRules;
        }

        public async Task<GetByIdAttendanceLogResponse> Handle(GetByIdAttendanceLogQuery request, CancellationToken cancellationToken)
        {
            AttendanceLog? attendanceLog = await _attendanceLogRepository.GetAsync(
                predicate: al => al.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _attendanceLogBusinessRules.AttendanceLogShouldExistWhenSelected(attendanceLog);

            GetByIdAttendanceLogResponse response = _mapper.Map<GetByIdAttendanceLogResponse>(attendanceLog);
            return response;
        }
    }
}
