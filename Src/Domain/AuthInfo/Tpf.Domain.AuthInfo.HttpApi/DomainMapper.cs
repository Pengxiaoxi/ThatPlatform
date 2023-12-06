using Tpf.AutoMapper;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Domain.Entity;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    public class DomainMapperProfile : AutoMapperProfile
    {
        public DomainMapperProfile()
        {
            CreateMap<UserInfo, UserInfoOutputDto>();
        }
    }
}
