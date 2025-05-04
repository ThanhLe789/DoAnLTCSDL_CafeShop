using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal SellingPrice { get; set; }

        public Product() { }
        public Product(string productId, string name, decimal sellingPrice)
        {
            ProductId = productId;
            Name = name;
            SellingPrice = sellingPrice;
        }
    }
}
