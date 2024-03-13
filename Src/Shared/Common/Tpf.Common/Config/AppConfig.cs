using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.Config
{
    public partial class AppConfig
    {
        public const string SecurityKey = "App:Security";


        #region Database|ConnectionString

        public const string Database_Main = "Database:Main";
        public const string Database_Slave = "Database:Slave";


        public const string ConnectionString_Default = "Default";

        public const string ConnectionString_Mysql = "Mysql";

        public const string ConnectionString_MongoDB = "MongoDB";

        #endregion


        #region ORM
        public const string ORM_Main = "ORM:Main";

        public const string ORM_Slave = "ORM:Slave"; 
        #endregion


    }
}
