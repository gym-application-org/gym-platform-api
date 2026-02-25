using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DietTemplates.Commands.Create.Dtos;

public class CreateDietTemplateDayDto
{
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<CreateDietTemplateMealDto> Meals { get; set; } = new List<CreateDietTemplateMealDto>();
}
