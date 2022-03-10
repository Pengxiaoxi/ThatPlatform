using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseORM.MongoDB;

namespace ThatPlatform.BaseInfo.Applciation.Impl
{
    public class UserService : IUserService
    {
        #region Field
        protected readonly IMongoDBRepository<UserInfo> _userMgRepository;
        #endregion

        #region Ctor
        public UserService(IMongoDBRepository<UserInfo> userMgRepository)
        {
            this._userMgRepository = userMgRepository;
        }
        #endregion

        /// <summary>
        /// GetUserInfosAsync
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserInfo>> GetUserInfosAsync()
        {
            var userInfoList = await _userMgRepository.FindAsync(x => x.UserName != null);

            //var userInfoStr = userInfoList.ToJson();
            //Console.WriteLine(userInfoStr);
            //var newUserInfoList = BsonSerializer.Deserialize<List<UserInfo>>(userInfoStr);

            return userInfoList;
        }

        public async Task InsertAsync(UserInfo userInfo)
        {
            await _userMgRepository.InsertAsync(userInfo);
        }
    }
}
