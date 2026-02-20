using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DietTemplateMealItemRepository : EfRepositoryBase<DietTemplateMealItem, int, BaseDbContext>, IDietTemplateMealItemRepository
{
    public DietTemplateMealItemRepository(BaseDbContext context)
        : base(context) { }
}
