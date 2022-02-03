using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace pos
{
    //public static string sqlConnect
    class koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source = TATSUMAKI\\SQLEXPRESS; database = db_perpus; Integrated Security = true";
            return conn;
        }
    }

}
