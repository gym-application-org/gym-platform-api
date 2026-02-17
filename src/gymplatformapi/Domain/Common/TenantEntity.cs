using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class TenantEntity<T> : Entity<T>
    {
        public Guid TenantId { get; set; }

        public TenantEntity()
        {
            TenantId = Guid.NewGuid();
        }
        public TenantEntity(Guid tenantId)
        {
            TenantId = tenantId;
        }
        
    }
}
