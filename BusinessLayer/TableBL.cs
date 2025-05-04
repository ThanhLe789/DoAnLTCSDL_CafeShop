using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataLayer;
using TransferObject;
namespace BusinessLayer
{
    public class TableBL
    {
        private TableDL tableDL;

        public TableBL()
        {
            tableDL = new TableDL();
        }
        public bool AddTable(string tableName, int capacity)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Tên bàn không được trống");

            if (capacity <= 0 || capacity > 20)
                throw new ArgumentException("Sức chứa phải từ 1-20 người");

            return tableDL.AddTable(tableName, capacity);
        }
        public bool DeleteTable(string tableId)
        {
            if (string.IsNullOrWhiteSpace(tableId))
                throw new ArgumentException("Mã bàn không hợp lệ");

            return tableDL.DeleteTable(tableId);
        }

        public List<Table> GetAllTables()
        {
            return tableDL.LoadAllTables();
        }

        public List<Table> GetAvailableTables()
        {
            try
            {
                return tableDL.GetAvailableTables();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ: " + ex.Message);
            }
        }

        // Cập nhật trạng thái bàn
        public bool UpdateTableStatus(string tableId, string newStatus)
        {
            try
            {
                return tableDL.UpdateTableStatus(tableId, newStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ: " + ex.Message);
            }
        }
    }
}
