using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class DietTemplate : TenantEntity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public int? CaloriesTarget { get; set; }
    public int? ProteinGramsTarget { get; set; }
    public int? CarbGramsTarget { get; set; }
    public int? FatGramsTarget { get; set; }

    public bool IsActive { get; set; }

    public ICollection<DietTemplateDay> Days { get; set; } = new List<DietTemplateDay>();

    public DietTemplate() { }

    public DietTemplate(
        Guid tenantId,
        string name,
        string? description,
        int? caloriesTarget,
        int? proteinGramsTarget,
        int? carbGramsTarget,
        int? fatGramsTarget,
        bool isActive = true
    )
        : base(tenantId)
    {
        Name = name;
        Description = description;

        CaloriesTarget = caloriesTarget;
        ProteinGramsTarget = proteinGramsTarget;
        CarbGramsTarget = carbGramsTarget;
        FatGramsTarget = fatGramsTarget;

        IsActive = isActive;
    }
}
