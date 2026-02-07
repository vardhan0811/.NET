using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q03_PaymentGateway;
using M1.Practice.Domain.Q03_PaymentGateway;

namespace M1.Practice.Tests.Q03_PaymentGateway
{
    [TestClass]
    public class PaymentGatewayTests
    {
        // ---------------------------------------------
        // Fake for Retry Test (Fail twice, then succeed)
        // ---------------------------------------------
        private class RetrySuccessGateway : ResilientPaymentService
        {
            private int _count;

            protected override async Task<bool> CallGatewayAsync(
                PaymentRequest request,
                CancellationToken ct)
            {
                await Task.Delay(5, ct);

                _count++;

                // Fail first 2 times
                if (_count <= 2)
                    return false;

                return true;
            }
        }

        // ---------------------------------------------
        // Fake for Circuit Test (Always fail)
        // ---------------------------------------------
        private class AlwaysFailGateway : ResilientPaymentService
        {
            protected override async Task<bool> CallGatewayAsync(
                PaymentRequest request,
                CancellationToken ct)
            {
                await Task.Delay(5, ct);

                return false; // Always fail
            }
        }

        // ---------------------------------------------
        // Retry Test
        // ---------------------------------------------
        [TestMethod]
        public async Task RetryThenSuccess_ShouldPass()
        {
            var service = new RetrySuccessGateway();

            var result =
                await service.ProcessPaymentAsync(
                    new PaymentRequest(),
                    CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Success", result.Message);
        }

        // ---------------------------------------------
        // Circuit Breaker Test
        // ---------------------------------------------
        [TestMethod]
        public async Task CircuitBreaker_ShouldOpen()
        {
            var service = new AlwaysFailGateway();

            // First call -> 3 failures
            await service.ProcessPaymentAsync(
                new PaymentRequest(),
                CancellationToken.None);

            // Second call -> 3 more failures (>=5)
            await service.ProcessPaymentAsync(
                new PaymentRequest(),
                CancellationToken.None);

            // Now circuit should be open
            var result =
                await service.ProcessPaymentAsync(
                    new PaymentRequest(),
                    CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Circuit open", result.Message);
        }
    }
}
