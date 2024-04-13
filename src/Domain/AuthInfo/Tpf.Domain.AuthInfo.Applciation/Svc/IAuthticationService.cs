using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.Domain.AuthInfo.Applciation.Dto;

namespace Tpf.Domain.AuthInfo.Applciation.Svc
{
    /// <summary>
    /// IAuthticationService
    /// </summary>
    public interface IAuthticationService
    {
        /// <summary>
        /// CreateJwtToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateJwtToken(UserInfoOutputDto user);

    }
}
