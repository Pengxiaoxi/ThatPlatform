using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq.Expressions;
using Tpf.Common.Config;
using static Dapper.SqlMapper;

namespace Tpf.Dapper.Repository
{
    /// <summary>
    /// DapperRepository
    /// Dapper 原生方法 + 扩展 Dapper.Contrib
    /// https://github.com/DapperLib/Dapper.Contrib
    /// </summary>
    public class DapperRepository<T> : IDapperRepository<T> where T : class
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
                    //or ConfigHeler.Get(AppConfig.ConnectionString_Mysql)
                    return new MySqlConnection(_config.GetConnectionString(AppConfig.ConnectionString_Mysql)); // MySql
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
        public IQueryable<T> All()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
        {
            //return this.Db.QueryAsync<T>();
            throw new NotImplementedException();
        }

        public async Task Insert(T entity)
        {
            await Db.InsertAsync(entity);
        }

        public async void Insert(IEnumerable<T> entities)
        {
            await Db.InsertAsync(entities);
        }

        public async Task<bool> Update(T entity)
        {
            return await Db.UpdateAsync(entity);
        }

        public async Task<bool> Update(IEnumerable<T> entities)
        {
            return await Db.UpdateAsync(entities.AsList());
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await Db.DeleteAsync(entity);
        }

        public Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
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

        


        #endregion
    }
}
