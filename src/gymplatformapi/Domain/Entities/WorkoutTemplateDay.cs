using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class WorkoutTemplateDay : TenantEntity<int>
{
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<WorkoutTemplateDayExercise> Exercises { get; set; } = new List<WorkoutTemplateDayExercise>();

    public int WorkoutTemplateId { get; set; }
    public virtual WorkoutTemplate WorkoutTemplate { get; set; } = null!;

    public WorkoutTemplateDay() { }

    public WorkoutTemplateDay(Guid tenantId, int workoutTemplateId, int dayNumber, string title)
        : base(tenantId)
    {
        WorkoutTemplateId = workoutTemplateId;
        DayNumber = dayNumber;
        Title = title;
    }
}
