using Application.Features.AttendanceLogs.Constants;
using Application.Features.AttendanceLogs.Constants;
using Application.Features.AttendanceLogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.AttendanceLogs.Constants.AttendanceLogsOperationClaims;

namespace Application.Features.AttendanceLogs.Commands.Delete;

public class DeleteAttendanceLogCommand
    : IRequest<DeletedAttendanceLogResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, AttendanceLogsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAttendanceLogs"];

    public class DeleteAttendanceLogCommandHandler : IRequestHandler<DeleteAttendanceLogCommand, DeletedAttendanceLogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly AttendanceLogBusinessRules _attendanceLogBusinessRules;

        public DeleteAttendanceLogCommandHandler(
            IMapper mapper,
            IAttendanceLogRepository attendanceLogRepository,
            AttendanceLogBusinessRules attendanceLogBusinessRules
        )
        {
            _mapper = mapper;
            _attendanceLogRepository = attendanceLogRepository;
            _attendanceLogBusinessRules = attendanceLogBusinessRules;
        }

        public async Task<DeletedAttendanceLogResponse> Handle(DeleteAttendanceLogCommand request, CancellationToken cancellationToken)
        {
            AttendanceLog? attendanceLog = await _attendanceLogRepository.GetAsync(
                predicate: al => al.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _attendanceLogBusinessRules.AttendanceLogShouldExistWhenSelected(attendanceLog);

            await _attendanceLogRepository.DeleteAsync(attendanceLog!);

            DeletedAttendanceLogResponse response = _mapper.Map<DeletedAttendanceLogResponse>(attendanceLog);
            return response;
        }
    }
}
