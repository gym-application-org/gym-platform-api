using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DietAssignments.Queries.GetMyDietAssignmentById;

public class MyDietTemplateMealDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Order { get; set; }
    public string? Notes { get; set; }

    public ICollection<MyDietTemplateMealItemDto> Items { get; set; } = new List<MyDietTemplateMealItemDto>();
}
