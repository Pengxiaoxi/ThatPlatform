using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.Dapper.Repository
{
    /// <summary>
    /// IDapperRepository
    /// 暴露原生 IDbConnection 及接口，若需自定义可基于原生方法进行仓储默认接口的封装
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDapperRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<string>
    {
        /// <summary>
        /// GetDbConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection Db { get; }

    }
}
