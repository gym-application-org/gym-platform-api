using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WorkoutTemplates.Commands.Create.Dtos;

public class CreateWorkoutTemplateDayDto
{
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<CreateWorkoutTemplateDayExerciseDto> Exercises { get; set; } = new List<CreateWorkoutTemplateDayExerciseDto>();
}
