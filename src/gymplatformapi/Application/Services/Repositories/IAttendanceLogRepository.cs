using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IAttendanceLogRepository : IAsyncRepository<AttendanceLog, int>, IRepository<AttendanceLog, int> { }
