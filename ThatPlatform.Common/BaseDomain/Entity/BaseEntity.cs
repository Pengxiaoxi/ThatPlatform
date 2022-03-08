using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Common.BaseDomain.Entity
{
    public class BaseEntity<T> where T : class
    {
        public ObjectId Id { get; set; }


        public string CreatedUserId { get; set; }

        public string CreatedUserName { get; set; }

        public Guid TenantId { get; set; }
    }
}
