using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Category
    {
        public Category(string categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }
        public Category() { }

        public string CategoryId { get; set; }
        public string Name { get; set; }
    }
}
