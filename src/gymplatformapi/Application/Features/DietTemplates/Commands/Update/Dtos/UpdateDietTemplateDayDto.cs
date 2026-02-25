using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.DietTemplates.Commands.Create.Dtos;

namespace Application.Features.DietTemplates.Commands.Update.Dtos;

public class UpdateDietTemplateDayDto
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<UpdateDietTemplateMealDto> Meals { get; set; } = new List<UpdateDietTemplateMealDto>();
}
