using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Common.BaseDomain.Entity
{
    public class BaseEntity<T> where T : class
    {
        public T Id { get; set; }


    }
}
