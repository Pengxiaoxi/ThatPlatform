using Newtonsoft.Json;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.GrpcApplciation.Client.Dto;
using ThatPlatform.BaseInfo.GrpcApplciation.Client.Svc;
using ThatPlatform.Common.BaseDomain.Entity;
using ThatPlatform.Common.BaseDomain.Impl;
using ThatPlatform.Common.BaseORM.MongoDB;
using ThatPlatform.Grpc.Client;
using ThatPlatform.Infrastructure.CommonAttributes;
using ThatPlatform.Tools.UtilLibrary;

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
        #endregion

    }
}
