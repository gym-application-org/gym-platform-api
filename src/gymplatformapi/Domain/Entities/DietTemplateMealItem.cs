using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class DietTemplateMealItem : TenantEntity<int>
{
    public int Order { get; set; }

    public string FoodName { get; set; } = default!;
    public decimal? Quantity { get; set; } 
    public string? Unit { get; set; }      

    public int? Calories { get; set; }
    public int? ProteinG { get; set; }
    public int? CarbG { get; set; }
    public int? FatG { get; set; }

    public string? Note { get; set; }

    public int DietTemplateMealId { get; set; }
    public virtual DietTemplateMeal DietTemplateMeal { get; set; } = null!;

    public DietTemplateMealItem() { } 

    public DietTemplateMealItem(
        Guid tenantId,
        int dietTemplateMealId,
        int order,
        string foodName,
        decimal? quantity,
        string? unit,
        int? calories,
        int? proteinG,
        int? carbG,
        int? fatG,
        string? note) : base(tenantId)
    {
        DietTemplateMealId = dietTemplateMealId;
        Order = order;

        FoodName = foodName;
        Quantity = quantity;
        Unit = unit;

        Calories = calories;
        ProteinG = proteinG;
        CarbG = carbG;
        FatG = fatG;

        Note = note;
    }
}
