using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class DietTemplateMeal : TenantEntity<int>
{
    public string Name { get; set; } = default!;
    public int Order { get; set; }
    public string? Notes { get; set; }

    public ICollection<DietTemplateMealItem> Items { get; set; } = new List<DietTemplateMealItem>();

    public int DietTemplateDayId { get; set; }
    public virtual DietTemplateDay DietTemplateDay { get; set; } = null!;

    public DietTemplateMeal() { }

    public DietTemplateMeal(Guid tenantId, int dietTemplateDayId, string name, int order, string? notes)
        : base(tenantId)
    {
        DietTemplateDayId = dietTemplateDayId;
        Name = name;
        Order = order;
        Notes = notes;
    }
}
