using System;
using System.Threading;
using System.Threading.Tasks;
using QuickBite.Application.Results;

namespace QuickBite.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> AuthorizePaymentAsync(
            decimal amount,
            string customerId,
            CancellationToken ct);

        Task<PaymentResult> CapturePaymentAsync(
            string transactionId,
            CancellationToken ct);

        Task<RefundResult> RefundPaymentAsync(
            string transactionId,
            decimal amount,
            CancellationToken ct);

        event Func<PaymentNotification, Task> PaymentNotificationReceived;
    }
}
