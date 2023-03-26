﻿using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Tpf.ORM.Dapper.Repository
{
    /// <summary>
    /// DapperRepository + MySql
    /// </summary>
    public class DapperRepository : IDapperRepository
    {
        #region Fields
        private readonly IConfiguration _config;
        private readonly IDbConnection _connection;

        private readonly string ConnectionStringName = "Tpf_Mysql";
        #endregion

        #region Properties
        public IDbConnection DbConnection
        {
            get
            {
                if (_connection == null)
                {
                    return new MySqlConnection(_config.GetConnectionString(ConnectionStringName)); // MySql
                }
                return _connection;
            }
        }

        /// <summary>
        /// IDbTransaction
        /// </summary>
        public IDbTransaction DbTransaction { get; set; }

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
            _connection = this.DbConnection;
        }
        #endregion

        /// <summary>
        /// GetDbConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetDbConnection()
        {
            return this.DbConnection;
        }

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

        /// <summary>
        /// GetDbTransaction
        /// </summary>
        /// <returns></returns>
        public IDbTransaction GetDbTransaction()
        {
            return this.DbTransaction;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            DbTransaction?.Dispose();
            if (_connection.State == ConnectionState.Open)
            {
                _connection?.Close();
            }
        }
    }
}
