using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;
using System.Data.SqlClient;

namespace DataLayer
{
    public class ProductDL:DataProvider
    {
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                Connect();
                string sql = "SELECT productId, name, sellingPrice FROM Product";
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    decimal sellingPrice = reader["sellingPrice"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["sellingPrice"])
                                      : 0;

                    products.Add(new Product(
                        reader["productId"].ToString(),
                        reader["name"].ToString(),
                        Convert.ToDecimal(reader["sellingPrice"])
                    ));
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi truy vấn dữ liệu sản phẩm: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi lấy danh sách sản phẩm: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }

            return products;
        }

        public decimal GetProductPrice(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                throw new ArgumentException("ProductId không hợp lệ.");
            try
            {
                Connect();
                string sql = "SELECT sellingPrice FROM Product WHERE productId = @productId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@productId", productId);
                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    throw new KeyNotFoundException("Không tìm thấy sản phẩm.");
                return Convert.ToDecimal(result);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi truy vấn giá sản phẩm: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi lấy giá sản phẩm: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }

        // add product
        public List<Product_Add> GetAllProducts_Add()
        {
            List<Product_Add> products = new List<Product_Add>();

            try
            {
                Connect();
                string sql = @"SELECT productId, name, purchasePrice, sellingPrice, categoryId, supplierId FROM Product";
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product_Add
                    {
                        ProductId = reader["productId"].ToString(),
                        Name = reader["name"].ToString(),
                        PurchasePrice = reader["purchasePrice"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["purchasePrice"])
                                        : 0,
                        SellingPrice = reader["sellingPrice"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["sellingPrice"])
                                        : 0,
                        CategoryId = reader["categoryId"].ToString(),
                        SupplierId = reader["supplierId"].ToString()
                    });
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi truy vấn dữ liệu sản phẩm: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi lấy danh sách sản phẩm: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }

            return products;
        }
        public bool AddProduct(Product_Add product)
        {
            try
            {
                Connect();
                string sql = @"INSERT INTO Product (name, purchasePrice, sellingPrice, categoryId, supplierId) 
                                VALUES (@name, @purPrice, @sellPrice, @catId, @supId)";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@purPrice", product.PurchasePrice);
                cmd.Parameters.AddWithValue("@sellPrice", product.SellingPrice);
                cmd.Parameters.AddWithValue("@catId", product.CategoryId);
                cmd.Parameters.AddWithValue("@supId", product.SupplierId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi thêm sản phẩm: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }
        //deleteproduct
        public bool DeleteProduct(string productId)
        {
            try
            {
                Connect();
                // 1.Xóa hết các OrderItem tham chiếu đến product
                string sqlDetail = "DELETE FROM OrderItem WHERE productId = @id";
                using (SqlCommand cmdDetail = new SqlCommand(sqlDetail, cn))
                {
                    cmdDetail.Parameters.AddWithValue("@id", productId);
                    cmdDetail.ExecuteNonQuery();
                }
                // 2. Xóa record trong Product
                string sql = "DELETE FROM Product WHERE productId = @id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }

                
               


                
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi xóa sản phẩm: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }
    }
}
