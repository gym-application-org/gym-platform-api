using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WorkoutAssignments.Queries.GetMyWorkoutById;

public class MyWorkoutTemplateDayDto
{
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<MyWorkoutTemplateDayExerciseDto> Exercises { get; set; } = new List<MyWorkoutTemplateDayExerciseDto>();
}
