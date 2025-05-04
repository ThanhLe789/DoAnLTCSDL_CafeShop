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
using static TransferObject.Users;

namespace CoffeeShopSystem.Product
{
    public partial class AddProductFrm : Form
    {
        private CategoryBL categoryBL;
        private SupplierBL supplierBL;
        private ProductBL productBL;
        
        public AddProductFrm()
        {
            InitializeComponent();
            categoryBL = new CategoryBL();
            supplierBL= new SupplierBL();
            productBL = new ProductBL();
            
        }

        private void AddProductFrm_Load(object sender, EventArgs e)
        {
            cbCateId.DataSource = categoryBL.GetCategories();
            cbCateId.DisplayMember = "Name";
            cbCateId.ValueMember = "CategoryId";
            
            cbSupplierId.DataSource = supplierBL.GetSuppliers();
            cbSupplierId.DisplayMember = "Name";
            cbSupplierId.ValueMember = "SupplierId";
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            var prod = new Product_Add()
            {
                Name = txtProductName.Text.Trim(),
                PurchasePrice = decimal.Parse(txtPurchase.Text.Trim()),
                SellingPrice = decimal.Parse(txtSelling.Text.Trim()),
                CategoryId = cbCateId.SelectedValue.ToString(),
                SupplierId = cbSupplierId.SelectedValue.ToString()
            };

            if (productBL.AddProduct(prod))
            {
                MessageBox.Show("Thêm sản phẩm thành công.");
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm thất bại.");
            }
        }
    }
}
