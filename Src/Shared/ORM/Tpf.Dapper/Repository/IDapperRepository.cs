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
    /// <typeparam name="T"></typeparam>
    [Obsolete("不可用，扩展 Dapper.Contrib NuGet Package 存在较多问题")]
    public interface IDapperRepository<T> : IBaseRepository<T> where T : BaseEntity<string>
    {
        /// <summary>
        /// GetDbConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection Db { get; }

    }
}
