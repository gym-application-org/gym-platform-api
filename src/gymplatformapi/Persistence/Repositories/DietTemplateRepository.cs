using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DietTemplateRepository : EfRepositoryBase<DietTemplate, int, BaseDbContext>, IDietTemplateRepository
{
    public DietTemplateRepository(BaseDbContext context)
        : base(context) { }
}
