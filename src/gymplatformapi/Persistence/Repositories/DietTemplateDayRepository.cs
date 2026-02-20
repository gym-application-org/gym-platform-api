using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DietTemplateDayRepository : EfRepositoryBase<DietTemplateDay, int, BaseDbContext>, IDietTemplateDayRepository
{
    public DietTemplateDayRepository(BaseDbContext context)
        : base(context) { }
}
