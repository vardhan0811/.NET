namespace M1.Practice.Domain.Q03_PaymentGateway
{
    public class PaymentRequest
    {
        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string CustomerId { get; set; }
    }
}
