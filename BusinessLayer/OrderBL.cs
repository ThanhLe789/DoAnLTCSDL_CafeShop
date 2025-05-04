using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataLayer;
using TransferObject;
using ClosedXML.Excel;
using System.IO;
namespace BusinessLayer
{
    public class OrderBL
    {
        private OrderDL orderDL = new OrderDL();

        public string CreateOrder(string tableId, string userId)
        {
            return orderDL.CreateOrder(tableId, userId);
        }

        public void AddOrderItem(string orderId, string productId, int quantity, decimal unitPrice)
        {
            if (!orderDL.AddOrderItem(orderId, productId, quantity, unitPrice))
                throw new Exception("Không thể thêm sản phẩm vào đơn hàng");
        }

        

        public List<OrderItem> GetOrderItems(string orderId)
        {
            return orderDL.GetOrderItems(orderId);
        }
        public bool UpdateOrderTotal(string orderId, decimal totalAmount)
        {
            try
            {
                // Cập nhật tổng tiền vào Orders
                return orderDL.UpdateOrderTotal(orderId, totalAmount);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật tổng tiền: " + ex.Message);
            }
        }


        public void AppendOrderToExcel(DataTable orderItems, string filePath, string orderId, decimal totalAmount, decimal amountReceived, decimal change)
        {
            XLWorkbook workbook;
            IXLWorksheet worksheet;

            if (File.Exists(filePath))
            {
                workbook = new XLWorkbook(filePath);
                worksheet = workbook.Worksheet(1); // Giả sử có 1 sheet
            }
            else
            {
                workbook = new XLWorkbook();
                worksheet = workbook.Worksheets.Add("Hóa đơn");

                // Ghi tiêu đề nếu file mới
                worksheet.Cell(1, 1).Value = "Mã hóa đơn";
                worksheet.Cell(1, 2).Value = "Tên sản phẩm";
                worksheet.Cell(1, 3).Value = "Số lượng";
                worksheet.Cell(1, 4).Value = "Đơn giá";
                worksheet.Cell(1, 5).Value = "Thành tiền";
                worksheet.Cell(1, 6).Value = "Tổng";
                worksheet.Cell(1, 7).Value = "Khách đưa";
                worksheet.Cell(1, 8).Value = "Tiền thối lại";
            }

            int lastRow = worksheet.LastRowUsed().RowNumber() + 1;

            worksheet.Cell(lastRow, 1).Value = $"Mã hóa đơn: {orderId}";
            worksheet.Cell(lastRow, 9).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            lastRow++;

            foreach (DataRow item in orderItems.Rows)
            {
                worksheet.Cell(lastRow, 1).Value = orderId;
                worksheet.Cell(lastRow, 2).Value = item["ProductName"].ToString();
                worksheet.Cell(lastRow, 3).Value = Convert.ToInt32(item["Quantity"]);
                worksheet.Cell(lastRow, 4).Value = Convert.ToDecimal(item["UnitPrice"]);
                worksheet.Cell(lastRow, 5).Value = Convert.ToInt32(item["Quantity"]) * Convert.ToDecimal(item["UnitPrice"]);
                worksheet.Cell(lastRow, 6).Value = totalAmount;
                worksheet.Cell(lastRow, 7).Value = amountReceived;
                worksheet.Cell(lastRow, 8).Value = change;
                lastRow++;
            }

            worksheet.Cell(lastRow, 6).Value = $"Tổng: {totalAmount}";
            worksheet.Cell(lastRow, 7).Value = $"Khách đưa: {amountReceived}";
            worksheet.Cell(lastRow, 8).Value = $"Tiền thối lại: {change}";
            workbook.SaveAs(filePath);
            
        }

    }




}
    

 
