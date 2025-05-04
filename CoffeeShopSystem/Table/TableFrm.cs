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

namespace CoffeeShopSystem.Table
{
    public partial class TableFrm : Form
    {
        private TableBL tableBL = new TableBL();
        public TableFrm()
        {
            InitializeComponent();
            LoadTables();
        }
        private void LoadTables()
        {
            // Giả sử có phương thức GetAllTables() trong TableBL
            try
            {
                dgvTables.DataSource = tableBL.GetAllTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn: " + ex.Message);
            }
        }

        private void LoadTableList()
        {
            try
            {
                // Gọi từ Business Layer
                List <TransferObject.Table> tables = tableBL.GetAllTables();

                // Bind vào DataGridView
                dgvTables.DataSource = tables;

                // Hoặc bind thủ công nếu cần tùy chỉnh
                dgvTables.Rows.Clear();
                foreach (TransferObject.Table table in tables)
                {
                    dgvTables.Rows.Add(
                        table.TableId,
                        table.Name,
                        table.Status,
                        table.Capacity
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn: " + ex.Message);
            }
        }
        private void TableFrm_Load(object sender, EventArgs e)
        {

        }

        private void btAddTable_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = txtTableName.Text.Trim();
                int capacity = (int)nudCapacity.Value;

                if (new TableBL().AddTable(tableName, capacity))
                {
                    MessageBox.Show("Thêm bàn thành công!");
                    LoadTables(); // Refresh danh sách
                    txtTableName.Clear();
                    nudCapacity.Value = 4; // Reset về giá trị mặc định
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btDeleteTable_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa");
                return;
            }

            string tableId = dgvTables.SelectedRows[0].Cells["TableId"].Value.ToString();

            string tableName = dgvTables.SelectedRows[0].Cells["Name"].Value.ToString();

            if (MessageBox.Show($"Xác nhận xóa bàn '{tableName}'?",
                               "Xác nhận",
                               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (new TableBL().DeleteTable(tableId))
                    {
                        MessageBox.Show("Xóa bàn thành công");
                        LoadTables(); // Refresh danh sách
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        
    }
}
