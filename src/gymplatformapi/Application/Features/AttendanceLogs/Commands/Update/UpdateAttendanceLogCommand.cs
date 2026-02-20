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

namespace Application.Features.AttendanceLogs.Commands.Update;

public class UpdateAttendanceLogCommand
    : IRequest<UpdatedAttendanceLogResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }
    public Guid MemberId { get; set; }
    public int GateId { get; set; }

    public string[] Roles => [Admin, Write, AttendanceLogsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAttendanceLogs"];

    public class UpdateAttendanceLogCommandHandler : IRequestHandler<UpdateAttendanceLogCommand, UpdatedAttendanceLogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly AttendanceLogBusinessRules _attendanceLogBusinessRules;

        public UpdateAttendanceLogCommandHandler(
            IMapper mapper,
            IAttendanceLogRepository attendanceLogRepository,
            AttendanceLogBusinessRules attendanceLogBusinessRules
        )
        {
            _mapper = mapper;
            _attendanceLogRepository = attendanceLogRepository;
            _attendanceLogBusinessRules = attendanceLogBusinessRules;
        }

        public async Task<UpdatedAttendanceLogResponse> Handle(UpdateAttendanceLogCommand request, CancellationToken cancellationToken)
        {
            AttendanceLog? attendanceLog = await _attendanceLogRepository.GetAsync(
                predicate: al => al.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _attendanceLogBusinessRules.AttendanceLogShouldExistWhenSelected(attendanceLog);
            attendanceLog = _mapper.Map(request, attendanceLog);

            await _attendanceLogRepository.UpdateAsync(attendanceLog!);

            UpdatedAttendanceLogResponse response = _mapper.Map<UpdatedAttendanceLogResponse>(attendanceLog);
            return response;
        }
    }
}
