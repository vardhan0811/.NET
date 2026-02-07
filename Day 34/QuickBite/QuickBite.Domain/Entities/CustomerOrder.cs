using System;
using System.Collections.Generic;
using QuickBite.Domain.Enums;

namespace QuickBite.Domain.Entities
{
    public class CustomerOrder
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        public string RestaurantId { get; set; }

        public List<OrderItem> Items { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DeliveryFee { get; set; }

        public string DeliveryAddress { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? EstimatedDeliveryTime { get; set; }

        public string AssignedDriverId { get; set; }

        public string PaymentTransactionId { get; set; }

        public string FailureReason { get; set; }

        public CustomerOrder()
        {
            Items = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Created;
        }
    }
}
