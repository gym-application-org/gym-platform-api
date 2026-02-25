using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.DietTemplates.Commands.Create.Dtos;

namespace Application.Features.DietTemplates.Commands.Update.Dtos;

public class UpdateDietTemplateMealDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Order { get; set; }
    public string? Notes { get; set; }

    public ICollection<UpdateDietTemplateMealItemDto> Items { get; set; } = new List<UpdateDietTemplateMealItemDto>();
}
