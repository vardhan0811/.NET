using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using M1.Practice.Domain.Q03_PaymentGateway;

namespace M1.Practice.Application.Q03_PaymentGateway
{
    public class ResilientPaymentService
    {
        private readonly object _lock = new object();

        private readonly Queue<DateTime> _failures =
            new Queue<DateTime>();

        private DateTime? _circuitOpenUntil;

        // Simulated external call
        protected virtual async Task<bool>
            CallGatewayAsync(
                PaymentRequest request,
                CancellationToken ct)
        {
            await Task.Delay(300, ct);

            // Random failure (simulation)
            return new Random().Next(1, 4) != 1;
        }

        // -------------------------------------

        public async Task<PaymentResult>
            ProcessPaymentAsync(
                PaymentRequest request,
                CancellationToken ct)
        {
            // Circuit open?
            if (_circuitOpenUntil.HasValue &&
                DateTime.UtcNow < _circuitOpenUntil)
            {
                return new PaymentResult
                {
                    IsSuccess = false,
                    Message = "Circuit open"
                };
            }

            for (int i = 1; i <= 3; i++)
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    var success =
                        await CallGatewayAsync(
                            request, ct);

                    if (success)
                    {
                        ResetFailures();

                        return new PaymentResult
                        {
                            IsSuccess = true,
                            Message = "Success"
                        };
                    }

                    throw new TimeoutException();
                }
                catch
                {
                    RegisterFailure();

                    if (i == 3)
                        break;

                    await Task.Delay(
                        i * 500, ct);
                }
            }

            return new PaymentResult
            {
                IsSuccess = false,
                Message = "Payment failed"
            };
        }

        // -------------------------------------

        private void RegisterFailure()
        {
            lock (_lock)
            {
                var now = DateTime.UtcNow;

                _failures.Enqueue(now);

                // Remove old failures (>1 min)
                while (_failures.Count > 0 &&
                       now - _failures.Peek() >
                       TimeSpan.FromMinutes(1))
                {
                    _failures.Dequeue();
                }

                if (_failures.Count >= 5)
                {
                    _circuitOpenUntil =
                        now.AddSeconds(30);

                    _failures.Clear();
                }
            }
        }

        private void ResetFailures()
        {
            lock (_lock)
            {
                _failures.Clear();
                _circuitOpenUntil = null;
            }
        }
    }
}
