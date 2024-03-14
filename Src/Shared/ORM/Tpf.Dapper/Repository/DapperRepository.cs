using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using Sikiro.Dapper.Extension.MySql;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using Tpf.BaseRepository;
using Tpf.Common.Enum;
using Tpf.Domain.Base.Domain.Entity;
using Tpf.Utils;

namespace Tpf.Dapper.Repository
{
    /// <summary>
    /// Dapper
    /// Dapper 原生方法 + 扩展 Dapper.Contrib
    /// https://github.com/DapperLib/Dapper.Contrib
    /// </summary>
    public class DapperRepository<TEntity> :
        BaseRepository<TEntity>,
        IDapperRepository<TEntity>
        where TEntity : BaseEntity<string>
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
        /// Dapper
        /// </summary>
        /// <param name="config"></param>
        public DapperRepository(IConfiguration config)
        {
            _config = config;
            _connection = DbConnection;
        }
        #endregion

        #region Publich Method
        public override async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Db.QuerySet<TEntity>().Where(expression).GetAsync();
        }

        public override async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return (expression == null
                ? await Db.QuerySet<TEntity>().ToListAsync()
                : await Db.QuerySet<TEntity>().Where(expression).ToListAsync()
                ).ToList();
        }

        public override async Task<bool> InsertAsync(TEntity entity)
        {
            return (await Db.InsertAsync(entity)) > 0;
        }

        public override async Task<bool> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            //var result = false;
            //foreach (var entity in entities)
            //{
            //    result = (await Db.InsertAsync(entity)) > 0;
            //}

            //return result;

            Db.CommandSet<TEntity>()
                .BatchInsert(entities);

            return await Task.FromResult(true);
                
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            return await Db.UpdateAsync(entity);
        }

        public override async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            return (await Db.CommandSet<TEntity>()
                .Where(whereExpression)
                .UpdateAsync(updateExpression)) >= 0;
        }

        public override async Task<bool> DeleteAsync(TEntity entity)
        {
            return await Db.DeleteAsync(entity);
        }

        public override async Task<bool> DeleteByIdAsync(string id)
        {
            Expression<Func<TEntity, bool>> whereExpression = x => x.Id == id;

            return await Db.DeleteAsync(whereExpression);
        }

        public override async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Db.DeleteAsync(whereExpression);
        }

        public override async Task<bool> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            return await Db.UpdateAsync(entities.AsList());
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            var result = Db.QuerySet<TEntity>()
                .Where(whereExpression)
                .Count();
            return await Task.FromResult(result);
        }

        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            var result = Db.QuerySet<TEntity>()
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

            var conn = ConnectionString;

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