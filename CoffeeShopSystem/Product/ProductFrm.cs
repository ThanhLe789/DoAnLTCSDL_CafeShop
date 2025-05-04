using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;

namespace CoffeeShopSystem.Product
{
    public partial class ProductFrm : Form
    {
        private ProductBL productBL;
        public ProductFrm()
        {
            InitializeComponent();
            productBL = new ProductBL();
        }

        private void ProductFrm_Load(object sender, EventArgs e)
        {
            Debug.WriteLine($"IsAdmin = {SessionManager.IsAdmin}");
            LoadProducts();
            
          
        }
        private void LoadProducts()
        {
            var products = productBL.GetAllProducts();
            dgvProducts.DataSource = products;
        }

        private void btAddProduct_Click(object sender, EventArgs e)
        {
            var addFrm = new AddProductFrm();
            if (addFrm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void btDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                string id = dgvProducts.CurrentRow.Cells["productId"].Value.ToString();
                if (productBL.DeleteProduct(id))
                {
                    MessageBox.Show("Xóa sản phẩm thành công.");
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm thất bại.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
            }
        }
    }
}
