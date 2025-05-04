using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;
using System.Data.SqlClient;
using static TransferObject.Users;

namespace DataLayer
{
    public class UserDL:DataProvider
    {
        public User GetUserByCredentials(string username, string password)
            {
                try
                {
                    Connect();
                    string sql = @"SELECT userId, userName, password, type 
                             FROM Users 
                             WHERE userName = @username AND password = @password";

                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                userId: reader["userId"].ToString(),
                                userName: reader["userName"].ToString(),
                                password: reader["password"].ToString(),
                                type: reader["type"].ToString()
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
        // Trong lớp UserDL
        public User GetUserById(string userId)
        {
            try
            {
                Connect();
                string sql = @"SELECT userId, userName, password, type 
                     FROM Users 
                     WHERE userId = @userId";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            userId: reader["userId"].ToString(),
                            userName: reader["userName"].ToString(),
                            password: reader["password"].ToString(),
                            type: reader["type"].ToString()
                        );
                    }
                }
                return null; // Không tìm thấy user
            }
            finally
            {
                DisConnect();
            }
        }
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            try
            {
                Connect();
                string sql = "SELECT userId, userName, password, type FROM Users";
                SqlCommand cmd = new SqlCommand(sql, cn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User(
                            reader["userId"].ToString(),
                            reader["userName"].ToString(),
                            reader["password"].ToString(),
                            reader["type"].ToString()
                        ));
                    }
                }
            }
            finally
            {
                DisConnect();
            }
            return users;
        }
        public string GetNextUserId()
        {
            try
            {
                Connect();
                string sql = "SELECT ISNULL(MAX(CAST(SUBSTRING(userId, 2, LEN(userId)) AS INT)), 0) + 1 FROM Users";
                int nextId = Convert.ToInt32(new SqlCommand(sql, cn).ExecuteScalar());
                return $"U{nextId:000}";
            }
            finally
            {
                DisConnect();
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                Connect();
                // Bỏ userId trong câu lệnh INSERT
                string sql = @"INSERT INTO Users (userName, password, type) 
                    VALUES (@userName, @password, @type)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@type", user.Type);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new Exception("Tên đăng nhập đã tồn tại");
            }
            finally
            {
                DisConnect();
            }
        }
            public bool DeleteUser(string userId)
        {
            try
            {
                Connect();

                // Kiểm tra đơn hàng liên quan
                string checkOrderSql = "SELECT COUNT(*) FROM Orders WHERE userId = @userId";
                SqlCommand checkOrderCmd = new SqlCommand(checkOrderSql, cn);
                checkOrderCmd.Parameters.AddWithValue("@userId", userId);

                if ((int)checkOrderCmd.ExecuteScalar() > 0)
                    throw new Exception("Không thể xóa nhân viên đã lập đơn hàng");

                // Thực hiện xóa
                string deleteSql = "DELETE FROM Users WHERE userId = @userId";
                SqlCommand deleteCmd = new SqlCommand(deleteSql, cn);
                deleteCmd.Parameters.AddWithValue("@userId", userId);

                return deleteCmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Không thể xóa nhân viên có dữ liệu liên quan");
            }
            finally
            {
                DisConnect();
            }
        }
    }
}
