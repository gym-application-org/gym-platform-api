using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DietTemplateMealRepository : EfRepositoryBase<DietTemplateMeal, int, BaseDbContext>, IDietTemplateMealRepository
{
    public DietTemplateMealRepository(BaseDbContext context)
        : base(context) { }
}
