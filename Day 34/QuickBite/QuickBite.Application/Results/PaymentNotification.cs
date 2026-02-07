using System;

namespace QuickBite.Application.Results
{
    public class PaymentNotification
    {
        public string TransactionId { get; set; }

        public bool IsSuccessful { get; set; }

        public DateTime ReceivedAt { get; set; }
    }
}
