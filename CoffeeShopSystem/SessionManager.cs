using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using static TransferObject.Users;

namespace CoffeeShopSystem
{
    public static class SessionManager
    {
        public static User CurrentUser { get; private set; }

        public static void Login(User user)
        {
            CurrentUser = new User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                // Don't store password in session
                Type = user.Type
            };
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsAdmin => CurrentUser?.Type?.ToLower() == "admin";
    }
}
