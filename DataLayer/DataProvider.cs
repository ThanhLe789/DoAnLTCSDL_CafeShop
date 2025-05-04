using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public class DataProvider
    {
        public SqlConnection cn;
        public SqlConnection GetSqlConnection()
        {
            return cn;
        }

        public DataProvider()
        {
            string cnstr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
            cn = new SqlConnection(cnstr);

        }

        public void Connect()
        {
            if (cn != null && cn.State == ConnectionState.Closed)
            {
                cn.Open();

            }
        }

        public void DisConnect()
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }
        public SqlDataReader MyExecuteReader(string sql, CommandType type)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = type;

            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int MyExecuteNonQuery(string sql, CommandType type, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = type;

            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                Connect();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                DisConnect();
            }
        }
    }
}
