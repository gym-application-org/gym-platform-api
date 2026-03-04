using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WorkoutAssignments.Queries.GetMyWorkoutById;

public class MyWorkoutTemplateDayExerciseDto
{
    public int Order { get; set; }

    public int Sets { get; set; }
    public string Reps { get; set; } = default!;
    public decimal? WeightKg { get; set; }
    public int? RestSeconds { get; set; }
    public string? Tempo { get; set; }
    public string? Note { get; set; }

    public int? ExerciseId { get; set; }
}
