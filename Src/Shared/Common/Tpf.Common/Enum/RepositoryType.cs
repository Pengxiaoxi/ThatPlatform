using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.Enum
{
    public enum RepositoryType
    {
        /// <summary>
        /// Dapper
        /// </summary>
        DapperRepository,

        /// <summary>
        /// EFCore
        /// </summary>
        EFRepository,

        /// <summary>
        /// SqlSuagr
        /// </summary>
        SqlSugarRepository,

        /// <summary>
        /// MongoDB
        /// </summary>
        MongoRepository,

    }
}
