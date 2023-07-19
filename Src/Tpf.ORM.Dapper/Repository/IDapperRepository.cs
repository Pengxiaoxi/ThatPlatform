using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.Common.BaseORM;

namespace Tpf.ORM.Dapper.Repository
{
    /// <summary>
    /// IDapperRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDapperRepository
    {
        /// <summary>
        /// GetDbConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetDbConnection();

        /// <summary>
        /// GetDbTransaction
        /// </summary>
        /// <returns></returns>
        IDbTransaction GetDbTransaction();

    }
}
