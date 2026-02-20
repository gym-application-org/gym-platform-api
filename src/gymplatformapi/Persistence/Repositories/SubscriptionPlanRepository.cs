using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubscriptionPlanRepository : EfRepositoryBase<SubscriptionPlan, int, BaseDbContext>, ISubscriptionPlanRepository
{
    public SubscriptionPlanRepository(BaseDbContext context)
        : base(context) { }
}
