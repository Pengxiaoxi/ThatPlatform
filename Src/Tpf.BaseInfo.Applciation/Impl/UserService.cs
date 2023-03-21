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

        //protected readonly BaseInfoDbContext _baseInfoDbContext;
        #endregion

        #region Ctor
        public UserService(ILogger<UserService<T>> log
            , IMongoDBRepository<T> repository

            //, BaseInfoDbContext baseInfoDbContext

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
            return await Task.FromResult(rsp);
        }

        public async Task<List<UserInfo>> GetUserInfoList()
        {
            using (var dbContext = new BaseInfoDbContext())
            {
                return dbContext.UserInfos.ToList();
            }
            
        }
        #endregion

    }
}
