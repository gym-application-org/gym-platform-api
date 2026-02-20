using Application.Features.AttendanceLogs.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.AttendanceLogs.Rules;

public class AttendanceLogBusinessRules : BaseBusinessRules
{
    private readonly IAttendanceLogRepository _attendanceLogRepository;
    private readonly ILocalizationService _localizationService;

    public AttendanceLogBusinessRules(IAttendanceLogRepository attendanceLogRepository, ILocalizationService localizationService)
    {
        _attendanceLogRepository = attendanceLogRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AttendanceLogsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AttendanceLogShouldExistWhenSelected(AttendanceLog? attendanceLog)
    {
        if (attendanceLog == null)
            await throwBusinessException(AttendanceLogsBusinessMessages.AttendanceLogNotExists);
    }

    public async Task AttendanceLogIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        AttendanceLog? attendanceLog = await _attendanceLogRepository.GetAsync(
            predicate: al => al.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AttendanceLogShouldExistWhenSelected(attendanceLog);
    }

    public async Task MemberShouldExistWhenSelected(Member? member)
    {
        if (member == null)
        {
            await throwBusinessException(AttendanceLogsBusinessMessages.MemberNotExists);
        }
    }

    public async Task TenantShouldExistWhenSelected(bool hasTenant)
    {
        if (!hasTenant)
        {
            await throwBusinessException(AttendanceLogsBusinessMessages.TenantNotExists);
        }
    }
}
