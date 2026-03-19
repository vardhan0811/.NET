namespace PaymentWebApi.Models;

public class Payment
{
    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }
}