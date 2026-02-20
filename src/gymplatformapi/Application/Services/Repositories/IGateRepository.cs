using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IGateRepository : IAsyncRepository<Gate, int>, IRepository<Gate, int> { }
