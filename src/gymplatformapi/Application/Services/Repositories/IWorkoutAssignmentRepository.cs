using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IWorkoutAssignmentRepository : IAsyncRepository<WorkoutAssignment, int>, IRepository<WorkoutAssignment, int> { }
