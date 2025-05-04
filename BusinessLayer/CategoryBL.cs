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
    public class CategoryBL
    {
       
        private CategoryDL categoryDL;

        public CategoryBL()
            {
                categoryDL = new CategoryDL();
            }
         private CategoryDL dl = new CategoryDL();
         public List<Category> GetCategories() => dl.GetAllCategories();
        
    }
}
