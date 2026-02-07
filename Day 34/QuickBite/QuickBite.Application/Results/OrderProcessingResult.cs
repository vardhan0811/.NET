using System;

namespace QuickBite.Application.Results
{
    public class OrderProcessingResult
    {
        public bool IsSuccess { get; set; }

        public string OrderId { get; set; }

        public string ErrorMessage { get; set; }

        public string FailureStage { get; set; }

        public DateTime? EstimatedDeliveryTime { get; set; }
    }
}
