using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ISubscriptionPlanRepository : IAsyncRepository<SubscriptionPlan, int>, IRepository<SubscriptionPlan, int> { }
