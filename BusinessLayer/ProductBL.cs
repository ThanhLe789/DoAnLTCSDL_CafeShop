using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TransferObject;
using DataLayer;
namespace BusinessLayer
{
    public class ProductBL
    {
        private ProductDL productDL;
        
        public ProductBL()
        {
            productDL = new ProductDL();
        }
        public List<Product> GetAllProducts()
        {
            return productDL.GetAllProducts();
        }

        public decimal GetProductPrice(string productId)
        {
            return productDL.GetProductPrice(productId);
        }

        //addproduct


        public List<Product_Add> GetAllProducts_Add()
        {
            return productDL.GetAllProducts_Add();
        }
        // Thêm sản phẩm mới
        public bool AddProduct(Product_Add product)
        {
            return productDL.AddProduct(product);
        }

        // Xóa sản phẩm
        public bool DeleteProduct(string productId)
        {
            return productDL.DeleteProduct(productId);
        }


     
        

        


    }
}
