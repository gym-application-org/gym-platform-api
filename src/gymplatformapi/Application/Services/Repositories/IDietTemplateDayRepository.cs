using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IDietTemplateDayRepository : IAsyncRepository<DietTemplateDay, int>, IRepository<DietTemplateDay, int> { }
