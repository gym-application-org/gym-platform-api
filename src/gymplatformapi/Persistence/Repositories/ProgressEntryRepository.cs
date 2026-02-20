using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProgressEntryRepository : EfRepositoryBase<ProgressEntry, int, BaseDbContext>, IProgressEntryRepository
{
    public ProgressEntryRepository(BaseDbContext context)
        : base(context) { }
}
