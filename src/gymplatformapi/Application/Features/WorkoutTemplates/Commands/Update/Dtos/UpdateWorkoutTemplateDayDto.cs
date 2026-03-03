using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkoutTemplates.Commands.Create.Dtos;

namespace Application.Features.WorkoutTemplates.Commands.Update.Dtos;

public class UpdateWorkoutTemplateDayDto
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<UpdateWorkoutTemplateDayExerciseDto> Exercises { get; set; } = new List<UpdateWorkoutTemplateDayExerciseDto>();
}
