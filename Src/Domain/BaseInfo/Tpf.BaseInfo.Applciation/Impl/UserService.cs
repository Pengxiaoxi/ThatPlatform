using Newtonsoft.Json;
using System.Threading.Tasks;
using Tpf.BaseInfo.Applciation.Svc;
using Tpf.BaseInfo.GrpcApplciation.Client.Dto;
using Tpf.BaseInfo.GrpcApplciation.Client.Svc;
using Tpf.Common.BaseDomain.Impl;
using Tpf.Common.BaseORM.MongoDB;
using Tpf.Grpc.Client;
using Tpf.Core.CommonAttributes;
using Tpf.Utils;
using Microsoft.Extensions.Logging;
using Tpf.Common.BaseDomain.Entity;
using System.Collections.Generic;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.BaseInfo.Domain;
using System.Linq;
using Tpf.BaseInfo.Applciation.Dto;
using System;
using Microsoft.EntityFrameworkCore;
using Tpf.ORM.Dapper.Repository;
using Dapper;

namespace Tpf.BaseInfo.Applciation.Impl
{
    /// <summary>
    /// UserService
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DependsOn(typeof(IUserService<>))]
    public class UserService<T> : BaseService<T>, IUserService<T> where T : BaseEntity<string>
    {
        #region Field
        private readonly IGrpcService _grpcService;

        private readonly IDapperRepository _dapperRepository;

        private readonly BaseInfoDbContext _dbContext;
        #endregion

        #region Ctor
        public UserService(ILogger<UserService<T>> log
            , IMongoDBRepository<T> repository
            , IDapperRepository dapperRepository
            , BaseInfoDbContext dbContext

            , IGrpcService grpcService
            ) : base(log, repository)
        {
            //_baseInfoDbContext = baseInfoDbContext;
            _dapperRepository = dapperRepository;

            _dbContext = dbContext;

            _grpcService = grpcService;

        }
        #endregion

        #region Public Method

        /// <summary>
        /// GetOrgByUserByGrpc
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetOrgByUserByGrpc()
        {
            var req = new GetOrgRequest() { OrgName = "pxx" };
            
            var orgGrpcServerAddress = ConfigHelper.GetConfig("gRpc:Organization");
            var _client = _grpcService.GetClient<IOrganizationService>(orgGrpcServerAddress);
            var rsp = await _client.GetOrganization(req);            

            System.Console.WriteLine(JsonConvert.SerializeObject(rsp)); ;
            return rsp;
        }

        public async Task<List<UserInfoOutputDto>> GetUserInfoList()
        {
            // inner join example
            var leftJoinQuery = from user in _dbContext.UserInfos
                                join dept in _dbContext.Depts
                                on user.DeptId equals dept.Id into grouping
                                from dept in grouping.DefaultIfEmpty()
                                select new UserInfoOutputDto()
                                {
                                    Account = user.Account,
                                    UserName = user.UserName,
                                    DeptName = dept.DeptName,
                                };
            var leftJoinResult = leftJoinQuery.ToList();

            //using (var dbContext = new BaseInfoDbContext())
            //{
            //    // inner join example
            //    var innerJoinQuery = from user in dbContext.UserInfos
            //                         join dept in dbContext.Depts
            //                         on user.DeptId equals dept.Id
            //                         select new UserInfoOutputDto()
            //                         {
            //                             Account = user.Account,
            //                             UserName = user.UserName,
            //                             DeptName = dept.DeptName,
            //                         };
            //    var innerJoinResult = innerJoinQuery.ToList();

            //    //// left join example
            //    //var leftJoinQuery = from user in dbContext.UserInfos
            //    //            join dept in dbContext.Depts
            //    //            on user.DeptId equals dept.Id into grouping
            //    //            from dept in grouping.DefaultIfEmpty()
            //    //            select new UserInfoOutputDto()
            //    //            {
            //    //                Account = user.Account,
            //    //                UserName = user.UserName,
            //    //                DeptName = dept.DeptName,
            //    //            };
            //    //var leftJoinResult = leftJoinQuery.ToList();

            //    // Execute Sql
            //    //var selectSql = "";

            //    //dbContext.UserInfos.FromSqlRaw(selectSql);
            //    //dbContext.UserInfos.FromSqlInterpolated($"{selectSql}");

            //    //await dbContext.Database.ExecuteSqlRawAsync(selectSql);
            //    //await dbContext.Database.ExecuteSqlInterpolatedAsync($"{selectSql}");

            //    var result = await this.GetUserInfoListByDapper();

            //    //var userList = dbContext.UserInfos.ToList();
            //    return result;
            //}

            var result = await this.GetUserInfoListByDapper();
            return result;

        }
        #endregion

        /// <summary>
        /// Dapper 查询示例
        /// </summary>
        /// <returns></returns>
        private async Task<List<UserInfoOutputDto>> GetUserInfoListByDapper()
        {
            var querySql = $"SELECT `t`.`Account`, `t`.`UserName`, `t0`.`DeptName` FROM `tpf_userinfo` AS `t` " +
                            $"LEFT JOIN `tpf_dept` AS `t0` " +
                            $"ON `t`.`DeptId` = `t0`.`Id`";
            var result = (await _dapperRepository.Db.QueryAsync<UserInfoOutputDto>(querySql)).ToList();
            return result;
        }
    }
}
