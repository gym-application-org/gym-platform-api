using System.Linq.Expressions;
using Application.Features.WorkoutAssignments.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutAssignments;

public class WorkoutAssignmentManager : IWorkoutAssignmentService
{
    private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
    private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;

    public WorkoutAssignmentManager(
        IWorkoutAssignmentRepository workoutAssignmentRepository,
        WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules
    )
    {
        _workoutAssignmentRepository = workoutAssignmentRepository;
        _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
    }

    public async Task<WorkoutAssignment?> GetAsync(
        Expression<Func<WorkoutAssignment, bool>> predicate,
        Func<IQueryable<WorkoutAssignment>, IIncludableQueryable<WorkoutAssignment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        WorkoutAssignment? workoutAssignment = await _workoutAssignmentRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutAssignment;
    }

    public async Task<IPaginate<WorkoutAssignment>?> GetListAsync(
        Expression<Func<WorkoutAssignment, bool>>? predicate = null,
        Func<IQueryable<WorkoutAssignment>, IOrderedQueryable<WorkoutAssignment>>? orderBy = null,
        Func<IQueryable<WorkoutAssignment>, IIncludableQueryable<WorkoutAssignment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<WorkoutAssignment> workoutAssignmentList = await _workoutAssignmentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutAssignmentList;
    }

    public async Task<WorkoutAssignment> AddAsync(WorkoutAssignment workoutAssignment)
    {
        WorkoutAssignment addedWorkoutAssignment = await _workoutAssignmentRepository.AddAsync(workoutAssignment);

        return addedWorkoutAssignment;
    }

    public async Task<WorkoutAssignment> UpdateAsync(WorkoutAssignment workoutAssignment)
    {
        WorkoutAssignment updatedWorkoutAssignment = await _workoutAssignmentRepository.UpdateAsync(workoutAssignment);

        return updatedWorkoutAssignment;
    }

    public async Task<WorkoutAssignment> DeleteAsync(WorkoutAssignment workoutAssignment, bool permanent = false)
    {
        WorkoutAssignment deletedWorkoutAssignment = await _workoutAssignmentRepository.DeleteAsync(workoutAssignment);

        return deletedWorkoutAssignment;
    }
}
