using Application.Features.AttendanceLogs.Constants;
using Application.Features.AttendanceLogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.AttendanceLogs.Constants.AttendanceLogsOperationClaims;

namespace Application.Features.AttendanceLogs.Commands.Create;

public class CreateAttendanceLogCommand
    : IRequest<CreatedAttendanceLogResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }
    public Guid MemberId { get; set; }
    public int GateId { get; set; }

    public string[] Roles => [Admin, Write, AttendanceLogsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAttendanceLogs"];

    public class CreateAttendanceLogCommandHandler : IRequestHandler<CreateAttendanceLogCommand, CreatedAttendanceLogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly AttendanceLogBusinessRules _attendanceLogBusinessRules;

        public CreateAttendanceLogCommandHandler(
            IMapper mapper,
            IAttendanceLogRepository attendanceLogRepository,
            AttendanceLogBusinessRules attendanceLogBusinessRules
        )
        {
            _mapper = mapper;
            _attendanceLogRepository = attendanceLogRepository;
            _attendanceLogBusinessRules = attendanceLogBusinessRules;
        }

        public async Task<CreatedAttendanceLogResponse> Handle(CreateAttendanceLogCommand request, CancellationToken cancellationToken)
        {
            AttendanceLog attendanceLog = _mapper.Map<AttendanceLog>(request);

            await _attendanceLogRepository.AddAsync(attendanceLog);

            CreatedAttendanceLogResponse response = _mapper.Map<CreatedAttendanceLogResponse>(attendanceLog);
            return response;
        }
    }
}
