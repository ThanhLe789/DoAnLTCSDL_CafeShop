using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Product_Add
    {
        public Product_Add(string productId, string name, decimal purchasePrice, decimal sellingPrice, string categoryId, string supplierId)
        {
            ProductId = productId;
            Name = name;
            PurchasePrice = purchasePrice;
            SellingPrice = sellingPrice;
            CategoryId = categoryId;
            SupplierId = supplierId;
        }
        public Product_Add() { }
        public string ProductId { get; set; }

        /// <summary> Tên sản phẩm </summary>
        public string Name { get; set; }

        /// <summary> Giá mua </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary> Giá bán </summary>
        public decimal SellingPrice { get; set; }

        /// <summary> Mã thể loại sản phẩm </summary>
        public string CategoryId { get; set; }

        /// <summary> Mã nhà cung cấp </summary>
        public string SupplierId { get; set; }
    }
}
