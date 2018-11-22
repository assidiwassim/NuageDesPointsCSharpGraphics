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
			return new SqlCeConnection(@"Data Source=C:\Users\wassim\Documents\GitHub\NuageDesPointsCSharpGraphics\DataBase.sdf");
		}
	}


}
