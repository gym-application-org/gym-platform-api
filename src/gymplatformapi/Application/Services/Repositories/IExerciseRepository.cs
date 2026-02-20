using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IExerciseRepository : IAsyncRepository<Exercise, int>, IRepository<Exercise, int> { }
