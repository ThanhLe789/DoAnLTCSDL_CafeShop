using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;
namespace BusinessLayer
{
    public class LoginBL
    {
        private LoginDL loginDL;

        public LoginBL()
        {
            loginDL = new LoginDL();
        }
        //public string Login(string username, string password)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(username))
        //            return "Chưa nhập tên đăng nhập";
        //        if (string.IsNullOrEmpty(password))
        //            return "Chưa nhập mật khẩu";

        //        bool isValid = loginDL.CheckLogin(username, password);
        //        return isValid ? "Đăng nhập thành công" : "Sai tài khoản hoặc mật khẩu";
        //    }
        //    catch (Exception ex) // Bắt mọi lỗi từ DL
        //    {
        //        return "Lỗi: " + ex.Message; // Trả về thông báo lỗi chi tiết
        //    }
        //}

        //public string GetUserType(string username)
        //{
        //    // Giả sử có hàm này trong LoginDL
            
        //    return loginDL.GetUserType(username);
        //}
    }
}
