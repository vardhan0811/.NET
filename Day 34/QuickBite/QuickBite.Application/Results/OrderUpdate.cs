using System;
using QuickBite.Domain.Enums;

namespace QuickBite.Application.Results
{
    public class OrderUpdate
    {
        public string OrderId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }
    }
}
