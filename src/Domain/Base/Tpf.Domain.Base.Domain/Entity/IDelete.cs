using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.Base.Domain.Entity
{
    internal interface IDelete
    {
        public bool IsDeleted { get; set; }

    }
}
