using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TransferObject;

namespace DataLayer
{
    public class SupplierDL:DataProvider
    {
        public List<Supplier> GetAllSuppliers()
        {
            var list = new List<Supplier>();
            try
            {
                Connect();
                string sql = "SELECT supplierId, name, address FROM Supplier";
                SqlCommand cmd = new SqlCommand(sql, cn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Supplier
                    {
                        SupplierId = reader["supplierId"].ToString(),
                        Name = reader["name"].ToString(),
                        Address = reader["address"].ToString()
                    });
                }
                reader.Close();
            }
            finally { DisConnect(); }
            return list;
        }
    }
}
