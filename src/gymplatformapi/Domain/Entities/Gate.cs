using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Gate : TenantEntity<int>
{
    public string Name { get; set; } = default!;
    public string GateCode { get; set; } = default!; 
    public bool IsActive { get; set; }

    public Gate() { }

    public Gate(Guid tenantId, string name, string gateCode, bool isActive = true)
        : base(tenantId)
    {
        Name = name;
        GateCode = gateCode;
        IsActive = isActive;
    }

}
