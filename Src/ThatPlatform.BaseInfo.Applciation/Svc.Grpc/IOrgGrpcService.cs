using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Dto.Grpc;

namespace ThatPlatform.BaseInfo.Applciation.Svc.Grpc
{
    [ServiceContract(Name = "IOrganizationService")]
    public interface IOrgGrpcService
    {
        [OperationContract]
        Task<GetOrgResposne> GetOrganization(GetOrgRequest req);
    }
}
