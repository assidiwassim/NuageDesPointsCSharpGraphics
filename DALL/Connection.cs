using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;


namespace DALL
{
    class Connection
    {
        public static SqlCeConnection GetConnection()
        {
            return new SqlCeConnection(@"Data Source=C:\Users\Assid\OneDrive\Documents\Visual Studio 2015\Projects\DAL\DataBase.sdf");
        }
    }


}
