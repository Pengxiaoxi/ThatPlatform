﻿using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using Sikiro.Dapper.Extension.MySql;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Xml.Linq;
using Tpf.Common.Enum;
using Tpf.Domain.Base.Domain.Entity;
using Tpf.Security;
using Tpf.Utils;

namespace Tpf.Dapper.Repository
{
    /// <summary>
    /// DapperRepository
    /// Dapper 原生方法 + 扩展 Dapper.Contrib
    /// https://github.com/DapperLib/Dapper.Contrib
    /// </summary>
    public class DapperRepository<T> : IDapperRepository<T> where T : BaseEntity<string>
    {
        #region Fields
        private readonly IConfiguration _config;
        private readonly IDbConnection _connection;


        #endregion

        #region Properties
        /// <summary>
        /// GetDbConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection Db
        {
            get { return DbConnection; }
        }

        private IDbConnection DbConnection
        {
            get
            {
                if (_connection == null)
                {
                    var db = GetDbConnection();

                    return db;
                }
                return _connection;
            }
        }

        /// <summary>
        /// IDbTransaction
        /// </summary>
        private IDbTransaction DbTransaction { get; set; }

        /// <summary>
        /// 事务是否已被提交
        /// </summary>
        public bool Committed { get; private set; } = true;
        #endregion

        #region Ctor
        /// <summary>
        /// DapperRepository
        /// </summary>
        /// <param name="config"></param>
        public DapperRepository(IConfiguration config)
        {
            _config = config;
            _connection = DbConnection;
        }
        #endregion

        #region Publich Method
        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await Db.QuerySet<T>().Where(expression).GetAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? expression = null)
        {
            return (expression == null
                ? await Db.QuerySet<T>().ToListAsync()
                : await Db.QuerySet<T>().Where(expression).ToListAsync()
                ).ToList();
        }

        public async Task<bool> InsertAsync(T entity)
        {
            return (await Db.InsertAsync(entity)) > 0;
        }

        public async Task<bool> InsertManyAsync(IEnumerable<T> entities)
        {
            //var result = false;
            //foreach (var entity in entities)
            //{
            //    result = (await Db.InsertAsync(entity)) > 0;
            //}

            //return result;

            Db.CommandSet<T>()
                .BatchInsert(entities);

            return await Task.FromResult(true);
                
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await Db.UpdateAsync(entity);
        }

        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateExpression)
        {
            return (await Db.CommandSet<T>()
                .Where(whereExpression)
                .UpdateAsync(updateExpression)) >= 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await Db.DeleteAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            Expression<Func<T, bool>> whereExpression = x => x.Id == id;

            return await Db.DeleteAsync(whereExpression);
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.DeleteAsync(whereExpression);
        }

        public async Task<bool> UpdateManyAsync(IEnumerable<T> entities)
        {
            return await Db.UpdateAsync(entities.AsList());
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> whereExpression = null)
        {
            var result = Db.QuerySet<T>()
                .Where(whereExpression)
                .Count();
            return await Task.FromResult(result);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            var result = Db.QuerySet<T>()
                .Where(whereExpression)
                .Count() > 0;
            return await Task.FromResult(result);
        }

        #endregion

        #region Transaction Operate
        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            Committed = false;
            bool isClosed = Db.State == ConnectionState.Closed;
            if (isClosed) Db.Open();
            DbTransaction = Db.BeginTransaction();
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void CommitTransaction()
        {
            DbTransaction?.Commit();
            Committed = true;

            Dispose();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollBackTransaction()
        {
            DbTransaction?.Rollback();
            Committed = true;

            Dispose();
        } 
        #endregion

        #region Private Method
        /// <summary>
        /// Dispose
        /// </summary>
        private void Dispose()
        {
            DbTransaction?.Dispose();
            if (_connection.State == ConnectionState.Open)
            {
                _connection?.Close();
            }
        }

        private static DbConnection GetDbConnection()
        {
            var dbType = ConfigHelper.GetMainDB();

            //var conn = ConfigHelper.GetConnectionString(dbType.ToString()) ?? throw new Exception("未配置主数据库对应的连接字符串");

            var conn = ConfigHelper.Get($"ConnectionStrings:{dbType.ToString()}") ?? throw new Exception("未配置主数据库对应的连接字符串");

            conn = AESHelper.Decrypt(conn, ConfigHelper.GetSecorityKey());

            switch (dbType)
            {
                case DBType.MySql:
                    return new MySqlConnection(conn);
                case DBType.PgSql:
                    return new NpgsqlConnection(conn);
                case DBType.MongoDB:
                case DBType.SqlServer:
                default:
                    throw new Exception("获取 DbConnection 失败, 目前仅支持 Mysql|PgSql");
            }
        }

        #endregion
    }
}