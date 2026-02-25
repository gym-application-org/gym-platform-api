using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DietTemplates.Queries.GetById.Dtos;

public class GetByIdDietTemplateMealItemDto
{
    public int Id { get; set; }
    public int Order { get; set; }

    public string FoodName { get; set; } = default!;
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }

    public int? Calories { get; set; }
    public int? ProteinG { get; set; }
    public int? CarbG { get; set; }
    public int? FatG { get; set; }

    public string? Note { get; set; }
}
