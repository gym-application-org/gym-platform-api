using System.Linq.Expressions;
using Application.Features.AttendanceLogs.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.AttendanceLogs;

public class AttendanceLogManager : IAttendanceLogService
{
    private readonly IAttendanceLogRepository _attendanceLogRepository;
    private readonly AttendanceLogBusinessRules _attendanceLogBusinessRules;

    public AttendanceLogManager(IAttendanceLogRepository attendanceLogRepository, AttendanceLogBusinessRules attendanceLogBusinessRules)
    {
        _attendanceLogRepository = attendanceLogRepository;
        _attendanceLogBusinessRules = attendanceLogBusinessRules;
    }

    public async Task<AttendanceLog?> GetAsync(
        Expression<Func<AttendanceLog, bool>> predicate,
        Func<IQueryable<AttendanceLog>, IIncludableQueryable<AttendanceLog, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AttendanceLog? attendanceLog = await _attendanceLogRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return attendanceLog;
    }

    public async Task<IPaginate<AttendanceLog>?> GetListAsync(
        Expression<Func<AttendanceLog, bool>>? predicate = null,
        Func<IQueryable<AttendanceLog>, IOrderedQueryable<AttendanceLog>>? orderBy = null,
        Func<IQueryable<AttendanceLog>, IIncludableQueryable<AttendanceLog, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AttendanceLog> attendanceLogList = await _attendanceLogRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return attendanceLogList;
    }

    public async Task<AttendanceLog> AddAsync(AttendanceLog attendanceLog)
    {
        AttendanceLog addedAttendanceLog = await _attendanceLogRepository.AddAsync(attendanceLog);

        return addedAttendanceLog;
    }

    public async Task<AttendanceLog> UpdateAsync(AttendanceLog attendanceLog)
    {
        AttendanceLog updatedAttendanceLog = await _attendanceLogRepository.UpdateAsync(attendanceLog);

        return updatedAttendanceLog;
    }

    public async Task<AttendanceLog> DeleteAsync(AttendanceLog attendanceLog, bool permanent = false)
    {
        AttendanceLog deletedAttendanceLog = await _attendanceLogRepository.DeleteAsync(attendanceLog);

        return deletedAttendanceLog;
    }
}
