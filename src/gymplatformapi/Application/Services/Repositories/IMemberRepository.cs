using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IMemberRepository : IAsyncRepository<Member, Guid>, IRepository<Member, Guid> { }
