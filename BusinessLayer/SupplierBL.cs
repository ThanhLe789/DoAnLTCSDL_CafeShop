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
    public class SupplierBL
    {
        private SupplierDL supplierDL;

        public SupplierBL()
        {
            supplierDL = new SupplierDL();
        }
        
        private SupplierDL dl = new SupplierDL();
        public List<Supplier> GetSuppliers() => dl.GetAllSuppliers();
        
    }
}
