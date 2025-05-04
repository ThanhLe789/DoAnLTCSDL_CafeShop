using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TransferObject;
using static TransferObject.Users;

namespace DataLayer
{
    public class OrderDL : DataProvider
    {
        public User GetUserByCredentials(string username, string password)
        {
            try
            {
                Connect();
                string sql = "SELECT userId, userName, type FROM Users WHERE userName = @username AND password = @password";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password); // Trong thực tế nên hash password

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader["userId"].ToString(),
                            reader["userName"].ToString(),
                            "", // Không lưu password trong session
                            reader["type"].ToString()
                        );
                    }
                }
                return null;
            }
            finally
            {
                DisConnect();
            }
        }
        public string GetUserNameById(string userId)
        {
            try
            {
                Connect();
                string sql = "SELECT userName FROM Users WHERE userId = @userId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@userId", userId);
                return cmd.ExecuteScalar()?.ToString();
            }
            finally
            {
                DisConnect();
            }
        }
        public string CreateOrder(string tableId, string userId)
        {
            try
            {
                Connect();
                string sql = "INSERT INTO Orders(orderDate, tableId, userId, status, paymentStatus) VALUES(GETDATE(), @tableId, @userId, N'Chờ', N'Chưa thanh toán');SELECT TOP 1 orderId FROM Orders WHERE tableId = @tableId AND userId = @userId ORDER BY orderDate DESC";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@tableId", tableId);
                cmd.Parameters.AddWithValue("@userId", userId);


                return cmd.ExecuteScalar().ToString();



            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi tạo đơn hàng: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi tạo đơn hàng: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }

        public bool AddOrderItem(string orderId, string productId, int quantity, decimal unitPrice)
        {
            try
            {
                Connect();
                string sql = "INSERT INTO OrderItem (orderId, productId, quantity, unitPrice) " +
                            "VALUES (@orderId, @productId, @quantity, @unitPrice)";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@unitPrice", unitPrice);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi thêm sản phẩm vào đơn hàng: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi thêm sản phẩm: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }

        public List<OrderItem> GetOrderItems(string orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            try
            {
                Connect();
                string sql = "SELECT oi.orderItemId, oi.orderId, oi.productId, p.name AS productName, " +
                            "oi.quantity, oi.unitPrice " +
                            "FROM OrderItem oi " +
                            "JOIN Product p ON oi.productId = p.productId " +
                            "WHERE oi.orderId = @orderId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@orderId", orderId);

                Console.WriteLine("Executing query: " + cmd.CommandText); // Debug query
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orderItems.Add(new OrderItem(
                        reader["orderItemId"].ToString(),
                        reader["orderId"].ToString(),
                        reader["productId"].ToString(),
                        reader["productName"].ToString(),
                        Convert.ToInt32(reader["quantity"]),
                        Convert.ToDecimal(reader["unitPrice"])
                    ));
                }
                reader.Close();
                Console.WriteLine($"Total items loaded: {orderItems.Count}"); // Debug tổng số item

            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi truy vấn chi tiết đơn hàng: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi lấy chi tiết đơn hàng: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }

            return orderItems;
        }

        public bool RemoveOrderItem(string orderItemId)
        {
            try
            {
                Connect();
                string sql = "DELETE FROM OrderItem WHERE orderItemId = @orderItemId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@orderItemId", orderItemId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi xóa sản phẩm khỏi đơn hàng: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi xóa sản phẩm: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }

        public bool CheckoutOrder(string orderId, decimal totalAmount)
        {
            try
            {
                Connect();
                string sql = "UPDATE Orders SET totalAmount = @totalAmount, " +
                            "status = N'Hoàn thành', paymentStatus = N'Đã thanh toán' " +
                            "WHERE orderId = @orderId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi thanh toán đơn hàng: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi thanh toán: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }

        public decimal CalculateTotalAmount(string orderId)
        {
            try
            {
                Connect();
                string sql = "SELECT SUM(quantity * unitPrice) FROM OrderItem WHERE orderId = @orderId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                object result = cmd.ExecuteScalar();
                return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi tính tổng tiền đơn hàng: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi tính tổng tiền: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }

        public bool UpdateOrderTotal(string orderId, decimal totalAmount)
        {
            try
            {
                Connect();
                string sql = "UPDATE Orders SET totalAmount = @totalAmount WHERE orderId = @orderId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                DisConnect();
            }

        }

    }
}
