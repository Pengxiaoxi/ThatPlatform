using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.BaseInfo.Applciation.Dto.Grpc
{
    [DataContract]
    public class GetOrgRequest
    {
        [DataMember(Order = 1)]
        public string OrgName { get; set; }

    }

    [DataContract]
    public class GetOrgResposne
    {
        [DataMember(Order = 1)]
        public string OrgName { get; set; }

        [DataMember(Order = 2)]
        public string OrgCode { get; set; }

        [DataMember(Order = 3)]
        public string OrgAdmin { get; set; }

    }
}
