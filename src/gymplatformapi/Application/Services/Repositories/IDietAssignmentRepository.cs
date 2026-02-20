using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IDietAssignmentRepository : IAsyncRepository<DietAssignment, int>, IRepository<DietAssignment, int> { }
