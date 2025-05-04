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
    public class CategoryDL:DataProvider
    {
        public List<Category> GetAllCategories()
        {
            var list = new List<Category>();
            try
            {
                Connect();
                string sql = "SELECT categoryId, name FROM ProductCategory";
                SqlCommand cmd = new SqlCommand(sql, cn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Category
                    {
                        CategoryId = reader["categoryId"].ToString(),
                        Name = reader["name"].ToString()
                    });
                }
                reader.Close();
            }
            finally { DisConnect(); }
            return list;
        }
    }
}
