using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
namespace DataLayer
{
    public class LoginDL:DataProvider
    {

        //public bool CheckLogin(string username, string password)
        //{
        //    string sql = "SELECT * FROM Users WHERE userName = @username AND password = @password";

        //    try
        //    {
        //        Connect();
        //        SqlCommand cmd = new SqlCommand(sql, cn);
        //        cmd.Parameters.AddWithValue("@username", username);
        //        cmd.Parameters.AddWithValue("@password", password);

        //        object result = cmd.ExecuteScalar(); // Trả về giá trị đầu tiên
        //        return (result != null); // True nếu có kết quả
        //    }
        //    catch (SqlException sqlEx) // Bắt lỗi SQL cụ thể
        //    {
        //        throw new Exception("Lỗi kết nối database: " + sqlEx.Message);
        //    }
        //    finally
        //    {
        //        DisConnect(); // Luôn đóng kết nối
        //    }
        //}

        //public string GetUserType(string username)
        //{
        //    string sql = "SELECT type FROM Users WHERE userName = @username";

        //    try
        //    {
        //        Connect();
        //        SqlCommand cmd = new SqlCommand(sql, cn);
        //        cmd.Parameters.AddWithValue("@username", username);
                
        //        object result = cmd.ExecuteScalar();
        //        return result?.ToString() ?? ""; // Trả về chuỗi rỗng nếu không tìm thấy
        //    }
        //    finally
        //    {
        //        DisConnect();
        //    }
        //}
    }
}
