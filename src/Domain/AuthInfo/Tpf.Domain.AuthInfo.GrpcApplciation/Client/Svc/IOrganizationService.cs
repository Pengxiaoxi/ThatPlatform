using System.ServiceModel;
using System.Threading.Tasks;
using Tpf.Domain.AuthInfo.GrpcApplciation.Client.Dto;

namespace Tpf.Domain.AuthInfo.GrpcApplciation.Client.Svc
{
    [ServiceContract(Name = "OrganizationService")]
    public interface IOrganizationService
    {
        [OperationContract(Name = "GetOrganization")]
        Task<GetOrgResposne> GetOrganization(GetOrgRequest req);

    }
}
