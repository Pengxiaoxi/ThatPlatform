﻿using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tpf.Domain.AuthInfo.GrpcApplciation.Server.Dto;
using Tpf.Domain.AuthInfo.GrpcApplciation.Server.Svc;

namespace Tpf.Domain.AuthInfo.GrpcApplciation.Server.Impl
{
    /// <summary>
    /// OrganizationGrpcService
    /// </summary>
    public class OrganizationGrpcService : IOrganizationGrpcService
    {
        private readonly ILogger _logger;

        public OrganizationGrpcService(ILogger<OrganizationGrpcService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GetOrganization
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<GetOrgResposne> GetOrganization(GetOrgRequest req)
        {
            var resp = new GetOrgResposne()
            {
                OrgName = req.OrgName + "_gRpc",
                OrgCode = req.OrgName + "_gRpc",
                OrgAdmin = $"admin_{req.OrgName}",
                Remark = "Visit OrganizationGrpcService Successful !"
            };
            _logger.LogInformation($"{DateTime.Now} gRpc: OrganizationGrpcService Response");
            return await Task.FromResult(resp);
        }
    }
}
