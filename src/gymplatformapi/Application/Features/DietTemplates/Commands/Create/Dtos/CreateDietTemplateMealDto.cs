using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DietTemplates.Commands.Create.Dtos;

public class CreateDietTemplateMealDto
{
    public string Name { get; set; } = default!;
    public int Order { get; set; }
    public string? Notes { get; set; }

    public ICollection<CreateDietTemplateMealItemDto> Items { get; set; } = new List<CreateDietTemplateMealItemDto>();
}
