using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IWorkoutTemplateDayRepository : IAsyncRepository<WorkoutTemplateDay, int>, IRepository<WorkoutTemplateDay, int> { }
