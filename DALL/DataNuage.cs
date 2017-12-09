using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
namespace DALL
{
    public class DataNuage
    {
        public int x;
        public int y;
        public DataNuage(int x , int y)
        {
            this.x = x;
            this.y = y;
        }
        public static int Insert(int x , int y)
        {
            SqlCeConnection cnn = Connection.GetConnection();
            cnn.Open();
            string cm = "Insert into Data(x,y) values(?,?)";
            SqlCeCommand cmd = new SqlCeCommand(cm, cnn);
            cmd.Parameters.AddWithValue("x", x);
            cmd.Parameters.AddWithValue("y", y);
         
            int i = cmd.ExecuteNonQuery();
            cnn.Close();
            return i;
        }
        public static int Delete(int x, int y)
        {
            SqlCeConnection cnn = Connection.GetConnection();
            cnn.Open();
            string cm = "delete from Data where x=" + x + "and y="+y+";";
            SqlCeCommand cmd = new SqlCeCommand(cm, cnn);
            int i = cmd.ExecuteNonQuery();
            cnn.Close();
            return i;
        }

        public static DataTable Select()
        {
            SqlCeConnection cnn = Connection.GetConnection();
            cnn.Open();
            string cm = "select x,y from Data;";
            SqlCeCommand cmd = new SqlCeCommand(cm, cnn);
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        
    }
}
