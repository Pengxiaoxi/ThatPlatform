using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Tpf.EntityFrameworkCore.Uow
{
    /// <summary>
    /// Uow
    /// TODO: 添加拦截器，通过 UnitOfWorkAttribute 实现 Aop 事务
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransaction();

        /// <summary>
        /// 保存 EFCore Repository 操作
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// GetConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();

        #region Dapper
        /// <summary>
        /// [Dapper] QueryAsync
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param = null, IDbContextTransaction trans = null) where TEntity : class;

        /// <summary>
        /// [Dapper] ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object param, IDbContextTransaction trans = null);

        #endregion


    }
}
