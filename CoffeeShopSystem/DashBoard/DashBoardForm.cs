using CoffeeShopSystem.Order;
using CoffeeShopSystem.StaffForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using TransferObject;
using BusinessLayer;
using static TransferObject.Users;
using CoffeeShopSystem.Table;
using CoffeeShopSystem.Product;

namespace CoffeeShopSystem.DashBoard
{
    public partial class DashBoardForm : Form
    {
        private Form _currentForm = null;
        private UserBL userBL;
        private User _currentUser;
        public DashBoardForm(User authenticatedUser)
        {
            InitializeComponent();
            userBL = new UserBL();
            _currentUser = authenticatedUser;
            InitializeUI();
        }
        private void InitializeUI()
        {
            // Hiển thị thông tin user
            lbWelcome.Text = $"Xin chào, {_currentUser.UserName}";

           
        }
        private void DashBoardForm_Load(object sender, EventArgs e)
        {
           
        }

        private void OpenChildForm(Form childForm)
        {
            // Đóng form hiện tại nếu có
            _currentForm?.Close();

            // Cấu hình form con
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Thêm vào panel
            pnLoadForm.Controls.Add(childForm);
            pnLoadForm.Tag = childForm;

            // Hiển thị
            childForm.BringToFront();
            childForm.Show();

            _currentForm = childForm;
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                            "Bạn có chắc muốn đăng xuất?",
                            "Xác nhận",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                // Đóng form Dashboard
                

                // Mở lại form Login
                FrmLogin loginForm = new FrmLogin();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        private void Order_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Order.OrderFrm(_currentUser.UserId));
        }
        // Staff

       
        private void btStaff_Click(object sender, EventArgs e)
        {
            if (_currentUser.Type.ToLower() == "admin")
            {
                // Mở form Staff nếu là admin
                OpenChildForm(new StaffFrm(_currentUser.UserId));
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này",
                              "Hạn chế quyền",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }

        private void DashBoardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        // xử lý nút home 

        
        private void btHome_Click(object sender, EventArgs e)
        {
            _currentForm?.Close();
            _currentForm = null;
        }

        private void btTable_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TableFrm());
        }

        private void btProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductFrm());
        }
    }
}
