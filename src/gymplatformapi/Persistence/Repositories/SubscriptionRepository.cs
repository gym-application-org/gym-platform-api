using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubscriptionRepository : EfRepositoryBase<Subscription, int, BaseDbContext>, ISubscriptionRepository
{
    public SubscriptionRepository(BaseDbContext context)
        : base(context) { }
}
