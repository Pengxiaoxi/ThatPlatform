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

        #endregion

        #region Ctor
        public UserService(ILogger<UserService<T>> log
            , IMongoDBRepository<T> repository


            , IGrpcService grpcService
            ) : base(log, repository)
        {
            //_baseInfoDbContext = baseInfoDbContext;

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

        public async Task<List<UserInfo>> GetUserInfoList()
        {
            using (var dbContext = new BaseInfoDbContext())
            {
                // inner join example
                var innerJoinQuery = from user in dbContext.UserInfos
                            join dept in dbContext.Depts
                            on user.DeptId equals dept.Id
                            select new UserInfoOutputDto()
                            {
                                Account = user.Account,
                                UserName = user.UserName,
                                DeptName = dept.DeptName,
                            };
                var innerJoinResult = innerJoinQuery.ToList();

                // left join example
                var leftJoinQuery = from user in dbContext.UserInfos
                            join dept in dbContext.Depts
                            on user.DeptId equals dept.Id into grouping
                            from dept in grouping.DefaultIfEmpty()
                            select new UserInfoOutputDto()
                            {
                                Account = user.Account,
                                UserName = user.UserName,
                                DeptName = dept.DeptName,
                            };
                var leftJoinResult = leftJoinQuery.ToList();

                // Execute Sql
                //var selectSql = "";

                //dbContext.UserInfos.FromSqlRaw(selectSql);
                //dbContext.UserInfos.FromSqlInterpolated($"{selectSql}");

                //await dbContext.Database.ExecuteSqlRawAsync(selectSql);
                //await dbContext.Database.ExecuteSqlInterpolatedAsync($"{selectSql}");


                var userList = dbContext.UserInfos.ToList();
                return userList;
            }
            
        }
        #endregion

    }
}
