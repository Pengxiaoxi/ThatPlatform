using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.CommonAttributes.Database
{
    /// <summary>
    /// DbCollectionAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MongoDbCollectionAttribute : Attribute
    {
        /// <summary>
        /// DatabaseName
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// TableName
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="DatabaseName"></param>
        /// <param name="TableName"></param>
        public MongoDbCollectionAttribute(string DatabaseName, string CollectionName)
        {
            this.DatabaseName = DatabaseName;
            this.CollectionName = CollectionName;
        }

    }
}
