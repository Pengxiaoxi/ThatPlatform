﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Common.BaseORM.SqlSuger
{
    public interface ISqlSugerRepository<T> : IBaseRepository<T> where T : class
    {
        
    }
}
