using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IUserActionTokenRepository : IAsyncRepository<UserActionToken, Guid>, IRepository<UserActionToken, Guid> { }
