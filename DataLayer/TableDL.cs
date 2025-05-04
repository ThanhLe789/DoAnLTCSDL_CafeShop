using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using TransferObject;
using System.Data.SqlClient;

namespace DataLayer
{
    public class TableDL : DataProvider
    {
        public string GetNextTableId()
        {
            try
            {
                Connect();
                string sql = "SELECT ISNULL(MAX(CAST(SUBSTRING(tableId, 2, LEN(tableId)) AS INT)), 0) + 1 FROM Tables";
                int nextId = Convert.ToInt32(new SqlCommand(sql, cn).ExecuteScalar());
                return $"T{nextId:000}"; // Format: T001, T002,...
            }
            finally
            {
                DisConnect();
            }
        }
        public bool AddTable(string tableName, int capacity)
        {
            try
            {
                string tableId = GetNextTableId();

                Connect();
                string sql = @"INSERT INTO Tables (tableId, name, status, capacity) 
                      VALUES (@tableId, @name, 'Có sẵn', @capacity)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@tableId", tableId);
                cmd.Parameters.AddWithValue("@name", tableName);
                cmd.Parameters.AddWithValue("@capacity", capacity);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm bàn: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }

        public bool DeleteTable(string tableId)
        {
            try
            {
                Connect();

                // Kiểm tra bàn có đang sử dụng không
                string checkSql = "SELECT COUNT(*) FROM Orders WHERE tableId = @tableId AND status <> 'Hoàn thành'";
                SqlCommand checkCmd = new SqlCommand(checkSql, cn);
                checkCmd.Parameters.AddWithValue("@tableId", tableId);

                if ((int)checkCmd.ExecuteScalar() > 0)
                    throw new Exception("Bàn đang có khách, không thể xóa");

                // Thực hiện xóa
                string deleteSql = "DELETE FROM Tables WHERE tableId = @tableId";
                SqlCommand deleteCmd = new SqlCommand(deleteSql, cn);
                deleteCmd.Parameters.AddWithValue("@tableId", tableId);

                return deleteCmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Không thể xóa bàn đã có lịch sử đơn hàng");
            }
            finally
            {
                DisConnect();
            }
        }

        public List<Table> LoadAllTables()
        {
            List<Table> tables = new List<Table>();

            try
            {
                Connect();
                string sql = "SELECT tableId, name, status, capacity FROM Tables ORDER BY name";
                SqlCommand cmd = new SqlCommand(sql, cn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(new Table
                        {
                            TableId = reader["tableId"].ToString(),
                            Name = reader["name"].ToString(),
                            Status = reader["status"].ToString(),
                            Capacity = Convert.ToInt32(reader["capacity"])
                        });
                    }
                }
            }
            finally
            {
                DisConnect();
            }

            return tables;
        }

        public List<Table> GetAvailableTables()
        {
            List<Table> tables = new List<Table>();
            try
            {
                Connect();
                string sql = "SELECT tableId, name, status, capacity FROM Tables";
                SqlCommand cmd = new SqlCommand(sql, cn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(new Table(
                            reader["tableId"].ToString(),
                            reader["name"].ToString(),
                            reader["status"].ToString(),
                            Convert.ToInt32(reader["capacity"])
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn bàn: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
            return tables;
        }

        // Cập nhật trạng thái bàn
        public bool UpdateTableStatus(string tableId, string newStatus)
        {
            try
            {
                Connect();
                string sql = "UPDATE Tables SET status = @newStatus WHERE tableId = @tableId";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@newStatus", newStatus);
                cmd.Parameters.AddWithValue("@tableId", tableId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật trạng thái bàn: " + ex.Message);
            }
            finally
            {
                DisConnect();
            }
        }
    }
}
