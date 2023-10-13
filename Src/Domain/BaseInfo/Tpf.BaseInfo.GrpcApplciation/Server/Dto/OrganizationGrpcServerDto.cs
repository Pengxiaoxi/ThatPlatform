using System.Runtime.Serialization;

namespace Tpf.BaseInfo.GrpcApplciation.Server.Dto
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
