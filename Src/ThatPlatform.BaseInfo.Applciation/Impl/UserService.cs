using System.Collections.Generic;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseDomain.Entity;
using ThatPlatform.Common.BaseDomain.Impl;
using ThatPlatform.Common.BaseDomain.Svc;
using ThatPlatform.Common.BaseORM.MongoDB;
using ThatPlatform.Common.Infrastructure.CommonAttributes;

namespace ThatPlatform.BaseInfo.Applciation.Impl
{
    /// <summary>
    /// UserService
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DependsOn(typeof(IUserService<>))]
    public class UserService<T> : BaseService<T>, IUserService<T> where T : BaseEntity<string>
    {
        #region Field
        
        #endregion

        #region Ctor
        public UserService(IMongoDBRepository<T> repository) : base(repository)
        {

        }
        #endregion

        #region Public Method

        #endregion

    }
}
