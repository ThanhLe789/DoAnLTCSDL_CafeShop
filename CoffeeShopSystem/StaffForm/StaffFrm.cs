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
using BusinessLayer;
using System.Data.SqlClient;
using static TransferObject.Users;

namespace CoffeeShopSystem.StaffForm
{
    public partial class StaffFrm : Form
    {
        private UserBL userBL;
        private string _currentUserId;
        public StaffFrm(string userId)
        {
            _currentUserId = userId;
            InitializeComponent();
            userBL = new UserBL();
            ConfigureDataGridView();
            LoadUsers();
        }

        private void LoadUsers()
        {
            dgvStaff.DataSource = userBL.GetAllUsers().Select(u => u).ToList(); ;
        }
        private void ConfigureDataGridView()
        {
            dgvStaff.AutoGenerateColumns = false;
            dgvStaff.Columns.Clear();

            // Cột ẩn chứa UserId
            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colUserId",
                DataPropertyName = "UserId",
                Visible = false
            });

            // Các cột hiển thị
            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colUserName",
                HeaderText = "Tên đăng nhập",
                DataPropertyName = "UserName",
                Width = 150
            });

            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colPassword",
                HeaderText = "Mật khẩu",
                DataPropertyName = "Password",
                Width = 150
            });

            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colType",
                HeaderText = "Loại tài khoản",
                DataPropertyName = "Type",
                Width = 120
            });
        }

        private void StaffFrm_Load(object sender, EventArgs e)
        {
           
        }


        private void btAddStaff_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra quyền Admin
                if (!userBL.IsAdmin(_currentUserId))
                {
                    MessageBox.Show("Chỉ Admin có quyền thêm nhân viên!",
                                  "Từ chối truy cập",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                // Mở form thêm nhân viên
                AddStaffForm addForm = new AddStaffForm();
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers(); // Refresh DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}",
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void btDeleteStaff_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
                return;
            }

            // Lấy thông tin từ DataGridView
            DataGridViewRow row = dgvStaff.SelectedRows[0];
            string userId = row.Cells["colUserId"].Value.ToString();
            string userName = row.Cells["colUserName"].Value.ToString();

            // Kiểm tra không cho xóa chính mình
            if (userId == _currentUserId)
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập",
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa
            if (MessageBox.Show($"Xác nhận xóa nhân viên '{userName}'?",
                               "Xác nhận",
                               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    userBL.DeleteUser(userId);
                    LoadUsers(); // Refresh danh sách
                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message,
                                   "Lỗi",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
        }
    }
}
