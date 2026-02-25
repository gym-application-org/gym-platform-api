using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DietTemplates.Queries.GetById.Dtos;

public class GetByIdDietTemplateDayDto
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<GetByIdDietTemplateMealDto> Meals { get; set; } = new List<GetByIdDietTemplateMealDto>();
}
