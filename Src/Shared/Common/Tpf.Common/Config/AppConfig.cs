using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.Config
{
    public partial class AppConfig
    {
        public const string Security = "App:Security";



        public const string ORM_MainORM = "ORM:MainORM";

        public const string SlaveORM = "ORM:SlaveORM";

        //public const string Database_DBType = "Database:DBType";

        //public const string Database_ConnectionString = "Database:ConnectionString";

        public const string ConnectionString_Mysql = "Mysql";
        public const string ConnectionString_MongoDB = "MongoDB";

    }
}
