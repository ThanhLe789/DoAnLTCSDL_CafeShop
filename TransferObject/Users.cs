using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Users
    {
        public class User
        {
            public string UserId { get; set; }       // VARCHAR(10)
            public string UserName { get; set; }    // NVARCHAR(50)
            public string Password { get; set; }     // NVARCHAR(255) - chỉ dùng khi đăng nhập
            public string Type { get; set; }        // NVARCHAR(50)

            // Constructor từ CSDL
            public User() { }
            public User(string userName, string password, string type)
            {
                UserName = userName;
                Password = password;
                Type = type;
            }
            public User(string userId, string userName, string password, string type)
            {
                UserId = userId;
                UserName = userName;
                Password = password; // Có thể null nếu không dùng đến
                Type = type; // Mặc định là staff nếu null
            }
        }
    }
}
