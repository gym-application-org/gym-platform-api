using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Tenant : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }

    public Tenant() { }

    public Tenant(string name, bool isActive = true)
        : base()
    {
        Name = name;
        IsActive = isActive;
    }
}
