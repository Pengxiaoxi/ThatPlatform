using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.BaseInfo.Applciation.Svc.Grpc
{
    public interface IOrgGrpcService
    {
        [OperationContract]
        Task<object> GetOrganization(object req);
    }
}
