using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DietAssignments.Queries.GetMyDietAssignmentById;

public class MyDietTemplateDayDto
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<MyDietTemplateMealDto> Meals { get; set; } = new List<MyDietTemplateMealDto>();
}
