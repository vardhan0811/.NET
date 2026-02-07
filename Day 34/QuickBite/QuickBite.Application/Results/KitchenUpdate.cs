using System;

namespace QuickBite.Application.Results
{
    public class KitchenUpdate
    {
        public string OrderId { get; set; }

        public string StationId { get; set; }

        public string Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
