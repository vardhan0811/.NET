using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuickBite.Application.Interfaces;
using QuickBite.Application.Results;
using QuickBite.Domain.Entities;
using QuickBite.Domain.Enums;
using QuickBite.Domain.Exceptions;

namespace QuickBite.Application.Orchestrators
{
    public class OrderProcessingEngine
    {
        private readonly IPaymentService _paymentService;
        private readonly IRestaurantService _restaurantService;
        private readonly IDriverService _driverService;
        private readonly ILogger<OrderProcessingEngine> _logger;

        public OrderProcessingEngine(
            IPaymentService paymentService,
            IRestaurantService restaurantService,
            IDriverService driverService,
            ILogger<OrderProcessingEngine> logger)
        {
            _paymentService = paymentService;
            _restaurantService = restaurantService;
            _driverService = driverService;
            _logger = logger;
        }

        public async Task<OrderProcessingResult> ProcessOrderAsync(
            CustomerOrder order,
            CancellationToken cancellationToken = default)
        {
            using var timeoutCts =
                CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            timeoutCts.CancelAfter(TimeSpan.FromMinutes(2));

            var ct = timeoutCts.Token;

            _logger.LogInformation(
                "Starting order {OrderId}",
                order.OrderId);

            try
            {
                //---------------------------
                // Step 1: Validate
                //---------------------------
                ValidateOrder(order);

                order.Status = OrderStatus.Created;

                //---------------------------
                // Step 2: Payment
                //---------------------------
                order.Status = OrderStatus.PaymentProcessing;

                var payment =
                    await ProcessPaymentAsync(order, ct);

                order.PaymentTransactionId =
                    payment.TransactionId;

                order.Status =
                    OrderStatus.PaymentSuccessful;

                //---------------------------
                // Step 3: Restaurant
                //---------------------------
                using var restaurantTimeout =
                    new CancellationTokenSource(
                        TimeSpan.FromSeconds(45));

                using var linked =
                    CancellationTokenSource
                        .CreateLinkedTokenSource(
                            ct,
                            restaurantTimeout.Token);

                var acceptance =
                    await _restaurantService
                        .AcceptOrderAsync(
                            order,
                            linked.Token);

                if (!acceptance.IsAccepted)
                    throw new Exception(
                        acceptance.Reason);

                order.Status =
                    OrderStatus.RestaurantAccepted;

                //---------------------------
                // Step 4: Driver
                //---------------------------
                var assignment =
                    await AssignDriverAsync(order, ct);

                order.AssignedDriverId =
                    assignment.Driver.DriverId;

                order.Status =
                    OrderStatus.DriverAssigned;

                //---------------------------
                // Step 5: Complete
                //---------------------------
                order.Status =
                    OrderStatus.Delivered;

                return new OrderProcessingResult
                {
                    IsSuccess = true,
                    OrderId = order.OrderId,
                    EstimatedDeliveryTime =
                        DateTime.UtcNow.AddMinutes(30)
                };
            }
            catch (OperationCanceledException)
            {
                order.Status = OrderStatus.Cancelled;

                _logger.LogWarning(
                    "Order {OrderId} cancelled",
                    order.OrderId);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Order {OrderId} failed",
                    order.OrderId);

                await CompensateAsync(order);

                return new OrderProcessingResult
                {
                    IsSuccess = false,
                    OrderId = order.OrderId,
                    ErrorMessage = ex.Message,
                    FailureStage =
                        order.Status.ToString()
                };
            }
        }

        // ------------------------
        // Helpers
        // ------------------------

        private void ValidateOrder(CustomerOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Items == null ||
                order.Items.Count == 0)
                throw new InvalidOrderException(
                    "Order must contain items");

            if (string.IsNullOrEmpty(order.RestaurantId))
                throw new InvalidOrderException(
                    "Restaurant missing");
        }

        private async Task<PaymentResult>
            ProcessPaymentAsync(
                CustomerOrder order,
                CancellationToken ct)
        {
            const int retries = 3;

            for (int i = 1; i <= retries; i++)
            {
                try
                {
                    var auth =
                        await _paymentService
                            .AuthorizePaymentAsync(
                                order.TotalAmount,
                                order.CustomerId,
                                ct);

                    if (!auth.IsAuthorized)
                        throw new Exception(
                            "Auth failed");

                    var capture =
                        await _paymentService
                            .CapturePaymentAsync(
                                auth.TransactionId,
                                ct);

                    if (!capture.IsSuccess)
                        throw new Exception(
                            "Capture failed");

                    return capture;
                }
                catch when (i < retries)
                {
                    await Task.Delay(
                        TimeSpan.FromSeconds(i * 2),
                        ct);
                }
            }

            throw new Exception(
                "Payment failed");
        }

        private async Task<DriverAssignment>
            AssignDriverAsync(
                CustomerOrder order,
                CancellationToken ct)
        {
            var drivers =
                await _driverService
                    .FindAvailableDriversAsync(
                        new Location(),
                        new Location(),
                        ct);

            if (drivers.Count == 0)
                throw new Exception(
                    "No drivers");

            var best = drivers[0];

            return await _driverService
                .AssignDriverAsync(
                    order.OrderId,
                    best.DriverId,
                    ct);
        }

        private async Task CompensateAsync(
            CustomerOrder order)
        {
            try
            {
                if (!string.IsNullOrEmpty(
                    order.PaymentTransactionId))
                {
                    await _paymentService
                        .RefundPaymentAsync(
                            order.PaymentTransactionId,
                            order.TotalAmount,
                            CancellationToken.None);
                }

                if (order.Status >=
                    OrderStatus.RestaurantAccepted)
                {
                    await _restaurantService
                        .CancelOrderAsync(
                            order.OrderId,
                            "System failure",
                            CancellationToken.None);
                }
            }
            catch
            {
                // Log only
            }
        }
    }
}
