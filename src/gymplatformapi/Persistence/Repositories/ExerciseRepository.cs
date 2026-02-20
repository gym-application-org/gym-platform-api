using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ExerciseRepository : EfRepositoryBase<Exercise, int, BaseDbContext>, IExerciseRepository
{
    public ExerciseRepository(BaseDbContext context)
        : base(context) { }
}
