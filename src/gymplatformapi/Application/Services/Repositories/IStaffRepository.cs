using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IStaffRepository : IAsyncRepository<Staff, Guid>, IRepository<Staff, Guid> { }
