using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AttendanceLogRepository : EfRepositoryBase<AttendanceLog, int, BaseDbContext>, IAttendanceLogRepository
{
    public AttendanceLogRepository(BaseDbContext context)
        : base(context) { }
}
