using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserActionTokenRepository : EfRepositoryBase<UserActionToken, Guid, BaseDbContext>, IUserActionTokenRepository
{
    public UserActionTokenRepository(BaseDbContext context)
        : base(context) { }
}
