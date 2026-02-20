using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IDietTemplateRepository : IAsyncRepository<DietTemplate, int>, IRepository<DietTemplate, int> { }
