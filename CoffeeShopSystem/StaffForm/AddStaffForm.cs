using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;
using System.Data.SqlClient;

namespace CoffeeShopSystem.StaffForm
{
    public partial class AddStaffForm : Form
    {
        private  UserBL userBL = new UserBL();
        public AddStaffForm()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                    throw new Exception("Vui lòng nhập tên đăng nhập");

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    throw new Exception("Vui lòng nhập mật khẩu");

                // Thêm nhân viên
                userBL.AddUser(
                    userName: txtUsername.Text.Trim(),
                    password: txtPassword.Text,
                    type: cbType.Text
                );

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {

            
            this.Close();
        }
    }
}
