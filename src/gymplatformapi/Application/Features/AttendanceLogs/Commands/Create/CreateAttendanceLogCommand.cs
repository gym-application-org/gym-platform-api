using Application.Features.AttendanceLogs.Constants;
using Application.Features.AttendanceLogs.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.AttendanceLogs.Constants.AttendanceLogsOperationClaims;

namespace Application.Features.AttendanceLogs.Commands.Create;

public class CreateAttendanceLogCommand
    : IRequest<CreatedAttendanceLogResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }
    public int GateId { get; set; }

    public string[] Roles => [Admin, Write, AttendanceLogsOperationClaims.Create, GeneralOperationClaims.Member];

    public class CreateAttendanceLogCommandHandler : IRequestHandler<CreateAttendanceLogCommand, CreatedAttendanceLogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly AttendanceLogBusinessRules _attendanceLogBusinessRules;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;
        private readonly ICurrentTenant _currentTenant;

        public CreateAttendanceLogCommandHandler(
            IMapper mapper,
            IAttendanceLogRepository attendanceLogRepository,
            AttendanceLogBusinessRules attendanceLogBusinessRules,
            ICurrentUser currentUser,
            IMemberService memberService,
            ICurrentTenant currentTenant
        )
        {
            _mapper = mapper;
            _attendanceLogRepository = attendanceLogRepository;
            _attendanceLogBusinessRules = attendanceLogBusinessRules;
            _currentUser = currentUser;
            _memberService = memberService;
            _currentTenant = currentTenant;
        }

        public async Task<CreatedAttendanceLogResponse> Handle(CreateAttendanceLogCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(
                predicate: x => x.UserId == _currentUser.UserId,
                cancellationToken: cancellationToken
            );
            await _attendanceLogBusinessRules.MemberShouldExistWhenSelected(member);

            AttendanceLog attendanceLog = _mapper.Map<AttendanceLog>(request);
            attendanceLog.MemberId = member!.Id;
            attendanceLog.TenantId = _currentTenant.TenantId!.Value;

            await _attendanceLogRepository.AddAsync(attendanceLog);

            CreatedAttendanceLogResponse response = _mapper.Map<CreatedAttendanceLogResponse>(attendanceLog);
            return response;
        }
    }
}
