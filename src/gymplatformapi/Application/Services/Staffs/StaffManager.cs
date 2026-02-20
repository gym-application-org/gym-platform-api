using System.Linq.Expressions;
using Application.Features.Staffs.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.Staffs;

public class StaffManager : IStaffService
{
    private readonly IStaffRepository _staffRepository;
    private readonly StaffBusinessRules _staffBusinessRules;

    public StaffManager(IStaffRepository staffRepository, StaffBusinessRules staffBusinessRules)
    {
        _staffRepository = staffRepository;
        _staffBusinessRules = staffBusinessRules;
    }

    public async Task<Staff?> GetAsync(
        Expression<Func<Staff, bool>> predicate,
        Func<IQueryable<Staff>, IIncludableQueryable<Staff, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Staff? staff = await _staffRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return staff;
    }

    public async Task<IPaginate<Staff>?> GetListAsync(
        Expression<Func<Staff, bool>>? predicate = null,
        Func<IQueryable<Staff>, IOrderedQueryable<Staff>>? orderBy = null,
        Func<IQueryable<Staff>, IIncludableQueryable<Staff, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Staff> staffList = await _staffRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return staffList;
    }

    public async Task<Staff> AddAsync(Staff staff)
    {
        Staff addedStaff = await _staffRepository.AddAsync(staff);

        return addedStaff;
    }

    public async Task<Staff> UpdateAsync(Staff staff)
    {
        Staff updatedStaff = await _staffRepository.UpdateAsync(staff);

        return updatedStaff;
    }

    public async Task<Staff> DeleteAsync(Staff staff, bool permanent = false)
    {
        Staff deletedStaff = await _staffRepository.DeleteAsync(staff);

        return deletedStaff;
    }
}
