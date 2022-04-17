using System.ServiceModel;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.GrpcApplciation.Server.Dto;
using ThatPlatform.Grpc.Server.Attributes;

namespace ThatPlatform.BaseInfo.GrpcApplciation.Server.Svc
{
    /// <summary>
    /// IOrganizationGrpcService
    /// </summary>
    [GrpcService]
    [ServiceContract(Name = "OrganizationService")]
    public interface IOrganizationGrpcService
    {
        /// <summary>
        /// GetOrganization
        /// 可不指定名称，客户端需要与服务端相同 Function Name
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetOrganization")]
        Task<GetOrgResposne> GetOrganization(GetOrgRequest req);

    }
}
