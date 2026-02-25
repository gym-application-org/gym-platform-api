using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DietTemplates.Queries.GetById.Dtos;

public class GetByIdDietTemplateMealDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Order { get; set; }
    public string? Notes { get; set; }

    public ICollection<GetByIdDietTemplateMealItemDto> Items { get; set; } = new List<GetByIdDietTemplateMealItemDto>();
}
