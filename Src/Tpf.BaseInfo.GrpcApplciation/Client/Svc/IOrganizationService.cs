using System.ServiceModel;
using System.Threading.Tasks;
using Tpf.BaseInfo.GrpcApplciation.Client.Dto;

namespace Tpf.BaseInfo.GrpcApplciation.Client.Svc
{
    [ServiceContract(Name = "OrganizationService")]
    public interface IOrganizationService
    {
        [OperationContract(Name = "GetOrganization")]
        Task<GetOrgResposne> GetOrganization(GetOrgRequest req);

    }
}
