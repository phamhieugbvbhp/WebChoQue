using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.Connect
{
    public class Data
    {
        public SqlConnection getconn()
        {
            return new SqlConnection(@"Data Source=HOANGUYEN;Initial Catalog=choqueVN.Com;Integrated Security=True");
        }
        public DataTable gettable(string sql)
        {
            SqlConnection conn = getconn(); conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void execute(string sql)
        {
            SqlConnection conn = getconn(); conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

        }
        public SqlDataReader dr(string sql)
        {
            SqlConnection conn = getconn(); conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd.ExecuteReader();
        }
    }
}
