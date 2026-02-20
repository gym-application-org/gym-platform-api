using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IDietTemplateMealItemRepository : IAsyncRepository<DietTemplateMealItem, int>, IRepository<DietTemplateMealItem, int> { }
