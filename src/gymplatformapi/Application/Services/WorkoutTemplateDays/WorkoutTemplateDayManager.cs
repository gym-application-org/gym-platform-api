using System.Linq.Expressions;
using Application.Features.WorkoutTemplateDays.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutTemplateDays;

public class WorkoutTemplateDayManager : IWorkoutTemplateDayService
{
    private readonly IWorkoutTemplateDayRepository _workoutTemplateDayRepository;
    private readonly WorkoutTemplateDayBusinessRules _workoutTemplateDayBusinessRules;

    public WorkoutTemplateDayManager(
        IWorkoutTemplateDayRepository workoutTemplateDayRepository,
        WorkoutTemplateDayBusinessRules workoutTemplateDayBusinessRules
    )
    {
        _workoutTemplateDayRepository = workoutTemplateDayRepository;
        _workoutTemplateDayBusinessRules = workoutTemplateDayBusinessRules;
    }

    public async Task<WorkoutTemplateDay?> GetAsync(
        Expression<Func<WorkoutTemplateDay, bool>> predicate,
        Func<IQueryable<WorkoutTemplateDay>, IIncludableQueryable<WorkoutTemplateDay, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        WorkoutTemplateDay? workoutTemplateDay = await _workoutTemplateDayRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutTemplateDay;
    }

    public async Task<IPaginate<WorkoutTemplateDay>?> GetListAsync(
        Expression<Func<WorkoutTemplateDay, bool>>? predicate = null,
        Func<IQueryable<WorkoutTemplateDay>, IOrderedQueryable<WorkoutTemplateDay>>? orderBy = null,
        Func<IQueryable<WorkoutTemplateDay>, IIncludableQueryable<WorkoutTemplateDay, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<WorkoutTemplateDay> workoutTemplateDayList = await _workoutTemplateDayRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutTemplateDayList;
    }

    public async Task<WorkoutTemplateDay> AddAsync(WorkoutTemplateDay workoutTemplateDay)
    {
        WorkoutTemplateDay addedWorkoutTemplateDay = await _workoutTemplateDayRepository.AddAsync(workoutTemplateDay);

        return addedWorkoutTemplateDay;
    }

    public async Task<WorkoutTemplateDay> UpdateAsync(WorkoutTemplateDay workoutTemplateDay)
    {
        WorkoutTemplateDay updatedWorkoutTemplateDay = await _workoutTemplateDayRepository.UpdateAsync(workoutTemplateDay);

        return updatedWorkoutTemplateDay;
    }

    public async Task<WorkoutTemplateDay> DeleteAsync(WorkoutTemplateDay workoutTemplateDay, bool permanent = false)
    {
        WorkoutTemplateDay deletedWorkoutTemplateDay = await _workoutTemplateDayRepository.DeleteAsync(workoutTemplateDay);

        return deletedWorkoutTemplateDay;
    }
}
