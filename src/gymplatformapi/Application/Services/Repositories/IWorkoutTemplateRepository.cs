using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IWorkoutTemplateRepository : IAsyncRepository<WorkoutTemplate, int>, IRepository<WorkoutTemplate, int> { }
