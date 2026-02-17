using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class WorkoutTemplate : TenantEntity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }

    public ICollection<WorkoutTemplateDay> Days { get; set; } = new List<WorkoutTemplateDay>();

    public WorkoutTemplate() { }

    public WorkoutTemplate(Guid tenantId, string name, string? description, DifficultyLevel level, bool isActive = true)
        : base(tenantId)
    {
        Name = name;
        Description = description;
        Level = level;
        IsActive = isActive;
    }
}
