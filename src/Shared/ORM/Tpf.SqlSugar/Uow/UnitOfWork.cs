using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Tpf.SqlSugar.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        private readonly ILogger<UnitOfWork> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        /// <param name="logger"></param>
        public UnitOfWork(ISqlSugarClient sqlSugarClient, ILogger<UnitOfWork> logger)
        {
            _sqlSugarClient = sqlSugarClient;
            _logger = logger;
        }

        /// <summary>
        /// 获取DB，保证唯一性
        /// </summary>
        /// <returns></returns>
        public SqlSugarScope GetDbClient()
        {
            // 必须要as，后边会用到切换数据库操作
            return _sqlSugarClient as SqlSugarScope;
        }

        public void BeginTran()
        {
            //GetDbClient().AsTenant().BeginTran();
            GetDbClient().BeginTran();
        }

        public void CommitTran()
        {
            try
            {
                //GetDbClient().AsTenant().CommitTran();
                GetDbClient().CommitTran();
            }
            catch (Exception ex)
            {
                GetDbClient().RollbackTran();
                _logger.LogError($"{ex.Message}\r\n{ex.InnerException}");
            }
        }

        public void RollbackTran()
        {
            GetDbClient().RollbackTran();
        }

    }

}
