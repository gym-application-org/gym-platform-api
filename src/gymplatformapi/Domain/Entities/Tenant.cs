using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Tenant : Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private Tenant() { }

    public Tenant(string name, bool isActive = true) : base()
    {
        Name = name;
        IsActive = isActive;
    }

}
