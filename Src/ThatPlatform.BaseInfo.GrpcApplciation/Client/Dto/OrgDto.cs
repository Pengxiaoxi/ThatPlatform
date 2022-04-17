using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.BaseInfo.GrpcApplciation.Client.Dto
{
    [DataContract]
    public class GetOrgRequest
    {
        [DataMember(Order = 1, Name = "orgName")]
        public string OrgName { get; set; }

    }

    [DataContract]
    public class GetOrgResposne
    {
        [DataMember(Order = 1, Name = "orgName")]
        public string OrgName { get; set; }

        [DataMember(Order = 2, Name = "orgCode")]
        public string OrgCode { get; set; }

        [DataMember(Order = 3, Name = "orgAdmin")]
        public string OrgAdmin { get; set; }

        [DataMember(Order = 4, Name = "remark")]
        public string Remark { get; set; }
    }
}
