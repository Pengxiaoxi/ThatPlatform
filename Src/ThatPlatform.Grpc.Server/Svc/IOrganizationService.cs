using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.Grpc.Server.Dto;

namespace ThatPlatform.Grpc.Server.Svc
{
    [ServiceContract]
    public interface IOrganizationService : IGrpcServiceBase
    {
        [OperationContract]
        Task<GetOrgResposne> GetOrganization(GetOrgRequest req);

    }
}
