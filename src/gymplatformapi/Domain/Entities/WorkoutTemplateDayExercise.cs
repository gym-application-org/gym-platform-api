using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class WorkoutTemplateDayExercise : TenantEntity<int>
{
    public int Order { get; set; }

    public int Sets { get; set; }
    public string Reps { get; set; } = default!;
    public decimal? WeightKg { get; set; }
    public int? RestSeconds { get; set; }
    public string? Tempo { get; set; }
    public string? Note { get; set; }

    public int WorkoutTemplateDayId { get; set; }
    public virtual WorkoutTemplateDay WorkoutTemplateDay { get; set; } = null!;

    public int? ExerciseId { get; set; }
    public virtual Exercise? Exercise { get; set; }

    public WorkoutTemplateDayExercise() { }

    public WorkoutTemplateDayExercise(
        Guid tenantId,
        int workoutTemplateDayId,
        int exerciseId,
        int order,
        int sets,
        string reps,
        decimal? weightKg,
        int? restSeconds,
        string? tempo,
        string? note
    )
        : base(tenantId)
    {
        WorkoutTemplateDayId = workoutTemplateDayId;
        ExerciseId = exerciseId;
        Order = order;

        Sets = sets;
        Reps = reps;
        WeightKg = weightKg;
        RestSeconds = restSeconds;
        Tempo = tempo;
        Note = note;
    }
}
