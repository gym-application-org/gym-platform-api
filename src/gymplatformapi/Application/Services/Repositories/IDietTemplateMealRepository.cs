using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IDietTemplateMealRepository : IAsyncRepository<DietTemplateMeal, int>, IRepository<DietTemplateMeal, int> { }
