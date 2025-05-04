using CoffeeShopSystem.DashBoard;
using CoffeeShopSystem.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TransferObject.Users;

namespace CoffeeShopSystem
{

     public static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           
                using (var loginForm = new FrmLogin())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {

                    // Sau này sẽ mở frmDashboard ở đây
                    Application.Run(new DashBoardForm(loginForm.AuthenticatedUser));
                }
                    else
                    {
                        Application.Exit();
                        return;
                    }
                }
           



        }
    }
}
