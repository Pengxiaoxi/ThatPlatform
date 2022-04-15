using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Dto.Grpc;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.Applciation.Svc.Grpc;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseDomain.Entity;
using ThatPlatform.Common.BaseDomain.Impl;
using ThatPlatform.Common.BaseDomain.Svc;
using ThatPlatform.Common.BaseORM.MongoDB;
using ThatPlatform.Grpc.Client;
using ThatPlatform.Infrastructure.CommonAttributes;

namespace ThatPlatform.BaseInfo.Applciation.Impl
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
        public UserService(IMongoDBRepository<T> repository
            , IGrpcService grpcService
            ) : base(repository)
        {
            _grpcService = grpcService;

        }
        #endregion

        #region Public Method

        /// <summary>
        /// GetOrgByUser
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetOrgByUser()
        {
            var req = new GetOrgRequest() { OrgName = "pxx-Grpc" };
            var orgGrpcServerAddress = "http://localhost:8001";

            var channel = _grpcService.GetChannel(orgGrpcServerAddress);
            var client = _grpcService.GetClient<IOrgGrpcService>(orgGrpcServerAddress);
            var rsp = client.GetOrganization(req);

            //System.Console.WriteLine(JsonConvert.SerializeObject(rsp)); ;

            return await Task.FromResult(rsp);
        }
        #endregion

    }
}
