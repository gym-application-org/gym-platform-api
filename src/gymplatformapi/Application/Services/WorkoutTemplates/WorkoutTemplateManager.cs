using System.Linq.Expressions;
using Application.Features.WorkoutTemplates.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutTemplates;

public class WorkoutTemplateManager : IWorkoutTemplateService
{
    private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
    private readonly WorkoutTemplateBusinessRules _workoutTemplateBusinessRules;

    public WorkoutTemplateManager(
        IWorkoutTemplateRepository workoutTemplateRepository,
        WorkoutTemplateBusinessRules workoutTemplateBusinessRules
    )
    {
        _workoutTemplateRepository = workoutTemplateRepository;
        _workoutTemplateBusinessRules = workoutTemplateBusinessRules;
    }

    public async Task<WorkoutTemplate?> GetAsync(
        Expression<Func<WorkoutTemplate, bool>> predicate,
        Func<IQueryable<WorkoutTemplate>, IIncludableQueryable<WorkoutTemplate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        WorkoutTemplate? workoutTemplate = await _workoutTemplateRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutTemplate;
    }

    public async Task<IPaginate<WorkoutTemplate>?> GetListAsync(
        Expression<Func<WorkoutTemplate, bool>>? predicate = null,
        Func<IQueryable<WorkoutTemplate>, IOrderedQueryable<WorkoutTemplate>>? orderBy = null,
        Func<IQueryable<WorkoutTemplate>, IIncludableQueryable<WorkoutTemplate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<WorkoutTemplate> workoutTemplateList = await _workoutTemplateRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutTemplateList;
    }

    public async Task<WorkoutTemplate> AddAsync(WorkoutTemplate workoutTemplate)
    {
        WorkoutTemplate addedWorkoutTemplate = await _workoutTemplateRepository.AddAsync(workoutTemplate);

        return addedWorkoutTemplate;
    }

    public async Task<WorkoutTemplate> UpdateAsync(WorkoutTemplate workoutTemplate)
    {
        WorkoutTemplate updatedWorkoutTemplate = await _workoutTemplateRepository.UpdateAsync(workoutTemplate);

        return updatedWorkoutTemplate;
    }

    public async Task<WorkoutTemplate> DeleteAsync(WorkoutTemplate workoutTemplate, bool permanent = false)
    {
        WorkoutTemplate deletedWorkoutTemplate = await _workoutTemplateRepository.DeleteAsync(workoutTemplate);

        return deletedWorkoutTemplate;
    }
}
