using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.BaseInfo.Domain.Entity
{
    public class Dept : BaseEntity<string>
    {

        public string Code { get; set; }

        public string DeptName { get; set; }


    }
}
