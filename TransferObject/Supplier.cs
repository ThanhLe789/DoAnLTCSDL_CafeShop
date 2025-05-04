using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Supplier
    {
        public Supplier(string supplierId, string name, string address)
        {
            SupplierId = supplierId;
            Name = name;
            Address = address;
        }
        public Supplier() { }

        public string SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
