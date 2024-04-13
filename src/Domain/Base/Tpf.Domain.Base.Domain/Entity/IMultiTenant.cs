using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.Base.Domain.Entity
{
    internal interface IMultiTenant
    {
        public Guid? TenantId { get; set; }
    }
}
