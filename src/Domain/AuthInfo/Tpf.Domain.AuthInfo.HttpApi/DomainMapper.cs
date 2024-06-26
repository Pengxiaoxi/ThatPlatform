﻿using Tpf.AutoMapper;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.Base.Domain.Context;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    /// <summary>
    /// DomainMapperProfile
    /// </summary>
    public class DomainMapperProfile : AutoMapperProfile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public DomainMapperProfile()
        {
            CreateMap<UserInfo, UserInfoOutputDto>();
            CreateMap<RegisterDto, UserInfo>();
            CreateMap<UserInfo, UserInfoOutputDto>();
            CreateMap<UserInfo, UserContextInfo>();

        }
    }
}
