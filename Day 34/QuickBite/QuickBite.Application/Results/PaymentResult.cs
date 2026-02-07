namespace QuickBite.Application.Results
{
    public class PaymentResult
    {
        public bool IsAuthorized { get; set; }

        public bool IsSuccess { get; set; }

        public string TransactionId { get; set; }

        public string FailureReason { get; set; }
    }
}
