using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class DietTemplateDay : TenantEntity<int>
{
    public int DayNumber { get; private set; }
    public string Title { get; private set; } = default!;
    public string? Notes { get; private set; }

    public ICollection<DietTemplateMeal> Meals = new List<DietTemplateMeal>();

    public int DietTemplateId { get; set; }
    public virtual DietTemplate DietTemplate { get; set; } = null!;

    public DietTemplateDay() { } 

    public DietTemplateDay(Guid tenantId, int dietTemplateId, int dayNumber, string title)
        : base(tenantId)
    {
        DietTemplateId = dietTemplateId;
        DayNumber = dayNumber;
        Title = title;
    }
}
