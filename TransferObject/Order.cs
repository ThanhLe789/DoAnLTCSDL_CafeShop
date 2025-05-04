using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Order
    {
        public string OrderId { get; set; }
        public string TableId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }

        public Order() { }
        public Order(string orderId, string tableId, string userId, decimal totalAmount,
                       string status, string paymentStatus)
        {
            OrderId = orderId;
            TableId = tableId;
            UserId = userId;
            TotalAmount = totalAmount;
            Status = status;
            PaymentStatus = paymentStatus;
        }
    }
}
