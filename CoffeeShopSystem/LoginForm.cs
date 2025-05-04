using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using CoffeeShopSystem.DashBoard;
using static TransferObject.Users;

namespace CoffeeShopSystem
{
    public partial class FrmLogin : Form
    {
        public User AuthenticatedUser { get; private set; }
        private UserBL userBL;
        public FrmLogin()
        {
            InitializeComponent();
            userBL = new UserBL();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private void btDangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                UserBL userBL = new UserBL();
                AuthenticatedUser = userBL.Authenticate(
                    txtUsername.Text.Trim(),
                    txtPassword.Text
                );

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }
    }
}
