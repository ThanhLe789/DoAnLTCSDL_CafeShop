using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataLayer;
using TransferObject;
using static TransferObject.Users;

namespace BusinessLayer
{
    public class UserBL
    {
        private UserDL userDL = new UserDL();
        public User Authenticate(string username, string password)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            // Get user from database
            User user = userDL.GetUserByCredentials(username, password);
            if (user == null)
                throw new UnauthorizedAccessException("Thông tin đăng nhập không chính xác");
            // Additional business logic can be added here
            return user;
        }
        public List<User> GetAllUsers() => userDL.GetAllUsers();

        public void AddUser(string userName, string password, string type)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Tên đăng nhập không được trống");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự");

            // Tạo user mới KHÔNG cần truyền userId
            User newUser = new TransferObject.Users.User(
                userName: userName,
                password: password, // Hash mật khẩu
                type: type.ToLower()
            );

            if (!userDL.AddUser(newUser))
                throw new Exception("Thêm người dùng thất bại");
        }

        // Thêm phương thức kiểm tra Admin
        public bool IsAdmin(string userId)
        {
            var user = userDL.GetUserById(userId); // Giả sử đã có phương thức này
            return user?.Type?.ToLower() == "admin";
        }
        public void DeleteUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Mã nhân viên không hợp lệ");

            if (!userDL.DeleteUser(userId))
                throw new Exception("Xóa nhân viên thất bại");
        }
    }
}
