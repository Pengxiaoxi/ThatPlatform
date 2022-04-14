using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.Grpc.Server.Dto;
using ThatPlatform.Grpc.Server.Svc;

namespace ThatPlatform.Grpc.Server.Impl
{
    public class OrganizationService : IOrganizationService
    {
        public async Task<GetOrgResposne> GetOrganization(GetOrgRequest req)
        {
            var resp = new GetOrgResposne()
            {
                OrgName = req.OrgName,
                OrgCode = req.OrgName,
                OrgAdmin = $"admin_{req.OrgName}"
            };
            return await Task.FromResult(resp);
        }
    }
}
