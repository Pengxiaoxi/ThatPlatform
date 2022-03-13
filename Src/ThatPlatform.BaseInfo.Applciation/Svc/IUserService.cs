using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Domain.Entity;

namespace ThatPlatform.BaseInfo.Applciation.Svc
{
    public interface IUserService
    {
        Task<List<UserInfo>> GetUserInfosAsync();

        Task InsertAsync(UserInfo userInfo);
    }
}
