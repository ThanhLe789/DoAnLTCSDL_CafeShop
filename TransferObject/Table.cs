using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Table
    {
        public string TableId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int Capacity { get; set; }
        public Table() { }
        public Table(string tableId, string name, string status, int capacity)
        {
            TableId = tableId;
            Name = name;
            Status = status;
            Capacity = capacity;
        }
    }


}
