using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class DietTemplateDay : TenantEntity<int>
{
    public int DayNumber { get; set; }
    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public ICollection<DietTemplateMeal> Meals { get; set; } = new List<DietTemplateMeal>();

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
