using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using TransferObject;
using System.IO;
namespace CoffeeShopSystem.Order
{
    public partial class OrderFrm : Form
    {
        private DataTable orderItems;
        private ProductBL productBL;
        private TableBL tableBL;
        private OrderBL orderBL;
        private string currentUserId; // Lấy từ login
        private string currentOrderId;
        public OrderFrm(string userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            productBL = new ProductBL();
            tableBL = new TableBL();
            orderBL = new OrderBL();
            orderItems = new DataTable();
        }
        private void OrderFrm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadAvailableTables();
            InitializeOrderItemsTable();
        }

        private void InitializeOrderItemsTable()
        {
            orderItems = new DataTable();
            orderItems.Columns.Add("ProductId", typeof(string));
            orderItems.Columns.Add("ProductName", typeof(string));
            orderItems.Columns.Add("Quantity", typeof(int));
            orderItems.Columns.Add("UnitPrice", typeof(decimal));
            

            dgvOrderItems.DataSource = orderItems;
            
        }
        private void LoadProducts()
        {
            try
            {
                dgvProducts.DataSource = productBL.GetAllProducts();
                dgvProducts.Columns["ProductId"].Visible = false;
                dgvProducts.Columns["Name"].HeaderText = "Tên sản phẩm";
                dgvProducts.Columns["SellingPrice"].HeaderText = "Giá bán";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        private void LoadAvailableTables()
        {
            try
            {
                var tables = tableBL.GetAvailableTables();
                if (tables.Count == 0)
                {
                    MessageBox.Show("Không có bàn trống!");
                }

                cbTables.DataSource = tables;
                cbTables.DisplayMember = "Name";
                cbTables.ValueMember = "TableId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load bàn: " + ex.Message);
            }
        }


        private void btAdd_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm!");
                return;
            }

            if (nudQuantity.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng lớn hơn 0!");
                return;
            }
            if (dgvProducts.SelectedRows.Count > 0)
            {
                string productId = dgvProducts.SelectedRows[0].Cells["ProductId"].Value.ToString();
                string name = dgvProducts.SelectedRows[0].Cells["Name"].Value.ToString();
                decimal price = Convert.ToDecimal(dgvProducts.SelectedRows[0].Cells["SellingPrice"].Value);
                int quantity = (int)nudQuantity.Value;

                try
                {
                    var existingRow = orderItems.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("ProductId") == productId);

                    if (existingRow != null)
                    {
                        existingRow["Quantity"] = (int)existingRow["Quantity"] + quantity;
                    }
                    else
                    {
                        // Kiểm tra số lượng cột trước khi add
                        if (orderItems.Columns.Count != 4)
                        {
                            MessageBox.Show("Lỗi: orderItems phải có đúng 4 cột (ProductId, ProductName, Quantity, UnitPrice)", "Lỗi cấu trúc bảng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
            

                        orderItems.Rows.Add(productId, name, quantity, price);
                    }

                    UpdateTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể thêm món vào danh sách đặt món.\nChi tiết: " + ex.Message,
                                    "Lỗi khi thêm món", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            if (dgvOrderItems.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvOrderItems.SelectedRows)
                {
                    orderItems.Rows.RemoveAt(row.Index);
                }
                UpdateTotal();
            }
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in orderItems.Rows)
            {
                int quantity = Convert.ToInt32(row["Quantity"]);
                decimal price = Convert.ToDecimal(row["UnitPrice"]);
                total += quantity * price;
            }
            lbTongTien.Text = total.ToString("N0") + " VNĐ";
        }
        private void btCheckOut_Click(object sender, EventArgs e)
        {

            if (cbTables.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bàn");
                return;
            }


            if (orderItems.Rows.Count == 0)
            {
                MessageBox.Show("Order không có sản phẩm nào");
                return;
            }

            try
            {
                // Tạo order mới
                string orderId = orderBL.CreateOrder(
                            cbTables.SelectedValue.ToString(),
                            currentUserId
                            );

                // Thêm các order item
                foreach (DataRow row in orderItems.Rows)
                {
                    orderBL.AddOrderItem(
                        orderId,
                        row["ProductId"].ToString(),
                        Convert.ToInt32(row["Quantity"]),
                        Convert.ToDecimal(row["UnitPrice"])
                                        );
                }

                // Cập nhật tổng tiền
                decimal total = decimal.Parse(lbTongTien.Text.Replace(" VNĐ", "").Replace(",", ""));
                orderBL.UpdateOrderTotal(orderId, total);
                // Cập nhật trạng thái bàn
                tableBL.UpdateTableStatus(cbTables.SelectedValue.ToString(), "Đang sử dụng");

                if (!decimal.TryParse(txtNhanTien.Text.Replace(",", ""), out decimal tienKhachDua))
                {
                    MessageBox.Show("Số tiền khách đưa không hợp lệ!");
                    return;
                }

                if (tienKhachDua < total)
                {
                    MessageBox.Show("Số tiền khách đưa không đủ để thanh toán!");
                    txtNhanTien.Clear();
                    return;
                }

                decimal tienThoi = tienKhachDua - total;

                MessageBox.Show($"Thanh toán thành công!\n\nTổng tiền: {total:N0} VNĐ\nTiền khách đưa: {tienKhachDua:N0} VNĐ\nTiền thối lại: {tienThoi:N0} VNĐ");
               


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }
        private void ResetForm()
        {
            orderItems.Rows.Clear();
            lbTongTien.Text = "0 VNĐ";
            nudQuantity.Value = 1;
            LoadAvailableTables();
            LoadProducts();
            InitializeOrderItemsTable();
            txtNhanTien.Clear();  // Xóa số tiền khách đưa
            cbTables.SelectedIndex = -1; // Chọn không có bàn
        }



        private void btThanhToan_Click(object sender, EventArgs e)
        {
            if (cbTables.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bàn");
                return;
            }

            if (orderItems.Rows.Count == 0)
            {
                MessageBox.Show("Order không có sản phẩm nào");
                return;
            }

            decimal total = decimal.Parse(lbTongTien.Text.Replace(" VNĐ", "").Replace(",", ""));
            if (!decimal.TryParse(txtNhanTien.Text, out decimal tienKhachDua))
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ.");
                return;
            }

            if (tienKhachDua < total)
            {
                MessageBox.Show("Số tiền khách đưa không đủ. Vui lòng nhập lại.");
                return;
            }

            try
            {
                // Tạo order mới
                string orderId = orderBL.CreateOrder(
                    cbTables.SelectedValue.ToString(),
                    currentUserId
                );

                // Thêm các order item
                foreach (DataRow row in orderItems.Rows)
                {
                    orderBL.AddOrderItem(
                        orderId,
                        row["ProductId"].ToString(),
                        Convert.ToInt32(row["Quantity"]),
                        Convert.ToDecimal(row["UnitPrice"])
                    );
                }

                // Cập nhật tổng tiền
                orderBL.UpdateOrderTotal(orderId, total);

                // Cập nhật trạng thái bàn
                tableBL.UpdateTableStatus(cbTables.SelectedValue.ToString(), "Đang sử dụng");

                decimal tienThoiLai = tienKhachDua - total;
                MessageBox.Show($"Thanh toán thành công!\nTổng tiền: {total.ToString("N0")} VNĐ\n" +
                                $"Tiền nhận: {tienKhachDua.ToString("N0")} VNĐ\n" +
                                $"Tiền thối lại: {tienThoiLai.ToString("N0")} VNĐ");

                string filePath = Path.Combine(Application.StartupPath, "HoaDon.xlsx");
                orderBL.AppendOrderToExcel(orderItems, filePath, orderId, total, tienKhachDua, tienThoiLai);
                MessageBox.Show($"Hóa đơn đã được lưu tại:\n{filePath}", "Xuất file Excel");
                ResetForm();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
