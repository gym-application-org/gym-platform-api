using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IProgressEntryRepository : IAsyncRepository<ProgressEntry, int>, IRepository<ProgressEntry, int> { }
