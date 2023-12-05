using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq.Expressions;
using Tpf.Common.Config;

namespace Tpf.Dapper.Repository
{
    /// <summary>
    /// DapperRepository
    /// </summary>
    public class DapperRepository<T> : IDapperRepository<T> where T : class
    {
        #region Fields
        private readonly IConfiguration _config;
        private readonly IDbConnection _connection;

        
        #endregion

        #region Properties
        private IDbConnection DbConnection
        {
            get
            {
                if (_connection == null)
                {
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
        /// GetDbConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection Db
        {
            get { return DbConnection; }
        }

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

        public Task InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            Committed = false;
            bool isClosed = DbConnection.State == ConnectionState.Closed;
            if (isClosed) DbConnection.Open();
            DbTransaction = DbConnection.BeginTransaction();
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
