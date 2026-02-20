using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.AttendanceLogs;

public interface IAttendanceLogService
{
    Task<AttendanceLog?> GetAsync(
        Expression<Func<AttendanceLog, bool>> predicate,
        Func<IQueryable<AttendanceLog>, IIncludableQueryable<AttendanceLog, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AttendanceLog>?> GetListAsync(
        Expression<Func<AttendanceLog, bool>>? predicate = null,
        Func<IQueryable<AttendanceLog>, IOrderedQueryable<AttendanceLog>>? orderBy = null,
        Func<IQueryable<AttendanceLog>, IIncludableQueryable<AttendanceLog, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AttendanceLog> AddAsync(AttendanceLog attendanceLog);
    Task<AttendanceLog> UpdateAsync(AttendanceLog attendanceLog);
    Task<AttendanceLog> DeleteAsync(AttendanceLog attendanceLog, bool permanent = false);
}
