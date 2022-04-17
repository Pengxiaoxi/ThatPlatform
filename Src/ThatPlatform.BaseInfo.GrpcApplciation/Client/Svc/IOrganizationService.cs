using System.ServiceModel;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.GrpcApplciation.Client.Dto;

namespace ThatPlatform.BaseInfo.GrpcApplciation.Client.Svc
{
    [ServiceContract(Name = "OrganizationService")]
    public interface IOrganizationService
    {
        [OperationContract(Name = "GetOrganization")]
        Task<GetOrgResposne> GetOrganization(GetOrgRequest req);

    }
}
