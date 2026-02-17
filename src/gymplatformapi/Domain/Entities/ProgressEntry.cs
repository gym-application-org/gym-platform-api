using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class ProgressEntry : TenantEntity<int>
{
    public DateTime Date { get; set; }

    public decimal? WeightKg { get; set; }
    public decimal? BodyFatPercent { get; set; }
    public decimal? MuscleMassKg { get; set; }

    public decimal? ChestCm { get; set; }
    public decimal? WaistCm { get; set; }
    public decimal? HipCm { get; set; }
    public decimal? ArmCm { get; set; }
    public decimal? LegCm { get; set; }

    public string? Note { get; set; }

    public Guid MemberId { get; set; }
    public virtual Member Member { get; set; } = null!;

    public ProgressEntry() { }

    public ProgressEntry(
        Guid tenantId,
        Guid memberId,
        DateTime date,
        decimal? weightKg,
        decimal? bodyFatPercent,
        decimal? muscleMassKg,
        decimal? chestCm,
        decimal? waistCm,
        decimal? hipCm,
        decimal? armCm,
        decimal? legCm,
        string? note
    )
        : base(tenantId)
    {
        MemberId = memberId;
        Date = date.Date;

        WeightKg = weightKg;
        BodyFatPercent = bodyFatPercent;
        MuscleMassKg = muscleMassKg;

        ChestCm = chestCm;
        WaistCm = waistCm;
        HipCm = hipCm;
        ArmCm = armCm;
        LegCm = legCm;

        Note = note;
    }
}
