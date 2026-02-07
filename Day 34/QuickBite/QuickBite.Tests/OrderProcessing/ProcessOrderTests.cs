using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuickBite.Application.Interfaces;
using QuickBite.Application.Orchestrators;
using QuickBite.Application.Results;
using QuickBite.Domain.Entities;

namespace QuickBite.Tests.OrderProcessing
{
    [TestClass]
    public class ProcessOrderTests
    {
        private Mock<IPaymentService> _paymentMock;
        private Mock<IRestaurantService> _restaurantMock;
        private Mock<IDriverService> _driverMock;
        private Mock<ILogger<OrderProcessingEngine>> _loggerMock;

        private OrderProcessingEngine _engine;

        [TestInitialize]
        public void Setup()
        {
            _paymentMock = new Mock<IPaymentService>();
            _restaurantMock = new Mock<IRestaurantService>();
            _driverMock = new Mock<IDriverService>();
            _loggerMock = new Mock<ILogger<OrderProcessingEngine>>();

            _engine = new OrderProcessingEngine(
                _paymentMock.Object,
                _restaurantMock.Object,
                _driverMock.Object,
                _loggerMock.Object);
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task ProcessOrder_WhenPaymentFailsTwiceThenSucceeds_ShouldSucceed()
        {
            var order = CreateValidOrder();

            _paymentMock
                .SetupSequence(p =>
                    p.AuthorizePaymentAsync(
                        It.IsAny<decimal>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .ThrowsAsync(new Exception())
                .ReturnsAsync(new PaymentResult
                {
                    IsAuthorized = true,
                    TransactionId = "TX123"
                });

            _paymentMock
                .Setup(p => p.CapturePaymentAsync(
                    "TX123",
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PaymentResult
                {
                    IsSuccess = true,
                    TransactionId = "TX123"
                });

            _restaurantMock
                .Setup(r => r.AcceptOrderAsync(
                    It.IsAny<CustomerOrder>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestaurantAcceptance
                {
                    IsAccepted = true
                });

            SetupDriver();

            var result = await _engine.ProcessOrderAsync(order);

            Assert.IsTrue(result.IsSuccess);

            _paymentMock.Verify(
                p => p.AuthorizePaymentAsync(
                    It.IsAny<decimal>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Exactly(3));
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task ProcessOrder_WhenPaymentAlwaysFails_ShouldFail()
        {
            var order = CreateValidOrder();

            _paymentMock
                .Setup(p => p.AuthorizePaymentAsync(
                    It.IsAny<decimal>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var result = await _engine.ProcessOrderAsync(order);

            Assert.IsFalse(result.IsSuccess);
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task ProcessOrder_WhenRestaurantRejects_ShouldFail()
        {
            var order = CreateValidOrder();

            SetupPaymentSuccess();

            _restaurantMock
                .Setup(r => r.AcceptOrderAsync(
                    It.IsAny<CustomerOrder>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestaurantAcceptance
                {
                    IsAccepted = false
                });

            var result = await _engine.ProcessOrderAsync(order);

            Assert.IsFalse(result.IsSuccess);
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task ProcessOrder_WhenNoDrivers_ShouldFail()
        {
            var order = CreateValidOrder();

            SetupPaymentAndRestaurant();

            _driverMock
                .Setup(d => d.FindAvailableDriversAsync(
                    It.IsAny<Location>(),
                    It.IsAny<Location>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<AvailableDriver>());

            var result = await _engine.ProcessOrderAsync(order);

            Assert.IsFalse(result.IsSuccess);
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task ProcessOrder_WithCancellation_ShouldCancel()
        {
            var order = CreateValidOrder();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            try
            {
                await _engine.ProcessOrderAsync(order, cts.Token);
                Assert.Fail("Expected cancellation exception");
            }
            catch (OperationCanceledException)
            {
                Assert.IsTrue(true);
            }
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task Kitchen_WhenStationFails_ShouldThrow()
        {
            var kitchen = new KitchenOrchestrator();

            var items = new List<OrderItem>
            {
                new OrderItem { Category = "Grill" }
            };

            var stations = new List<KitchenStation>
            {
                new KitchenStation
                {
                    StationType = "Grill",
                    IsOperational = false
                }
            };

            try
            {
                await kitchen.CookOrderAsync(
                    items,
                    stations,
                    CancellationToken.None);

                Assert.Fail("Expected exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task BatchProcessor_WhenPaused_ShouldWait()
        {
            var batch = new BatchOrderProcessor(_engine);

            batch.Pause();

            var orders = new List<CustomerOrder>
            {
                CreateValidOrder()
            };

            var task = batch.ProcessBatchAsync(
                orders,
                CancellationToken.None);

            await Task.Delay(1000);

            Assert.IsFalse(task.IsCompleted);

            batch.Resume();

            await task;
        }

        // ---------------------------------------------------

        [TestMethod]
        public async Task BatchProcessor_WhenCircuitOpen_ShouldFail()
        {
            var batch = new BatchOrderProcessor(_engine);

            var orders = new List<CustomerOrder>();

            for (int i = 0; i < 6; i++)
            {
                var o = CreateValidOrder();
                o.RestaurantId = "R1";
                orders.Add(o);
            }

            try
            {
                await batch.ProcessBatchAsync(
                    orders,
                    CancellationToken.None);

                Assert.Fail("Expected circuit breaker exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        // ---------------------------------------------------
        // Helpers
        // ---------------------------------------------------

        private CustomerOrder CreateValidOrder()
        {
            return new CustomerOrder
            {
                OrderId = Guid.NewGuid().ToString(),
                CustomerId = "C1",
                RestaurantId = "R1",
                TotalAmount = 500,
                DeliveryFee = 50,
                DeliveryAddress = "Hyderabad",

                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Burger",
                        Category = "Grill",
                        Price = 200,
                        Quantity = 1
                    }
                }
            };
        }

        private void SetupPaymentSuccess()
        {
            _paymentMock
                .Setup(p => p.AuthorizePaymentAsync(
                    It.IsAny<decimal>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PaymentResult
                {
                    IsAuthorized = true,
                    TransactionId = "TX1"
                });

            _paymentMock
                .Setup(p => p.CapturePaymentAsync(
                    "TX1",
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PaymentResult
                {
                    IsSuccess = true,
                    TransactionId = "TX1"
                });
        }

        private void SetupPaymentAndRestaurant()
        {
            SetupPaymentSuccess();

            _restaurantMock
                .Setup(r => r.AcceptOrderAsync(
                    It.IsAny<CustomerOrder>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestaurantAcceptance
                {
                    IsAccepted = true
                });
        }

        private void SetupDriver()
        {
            _driverMock
                .Setup(d => d.FindAvailableDriversAsync(
                    It.IsAny<Location>(),
                    It.IsAny<Location>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<AvailableDriver>
                {
                    new AvailableDriver
                    {
                        DriverId = "D1",
                        Rating = 5,
                        EstimatedArrivalMinutes = 5
                    }
                });

            _driverMock
                .Setup(d => d.AssignDriverAsync(
                    It.IsAny<string>(),
                    "D1",
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DriverAssignment
                {
                    IsAccepted = true,
                    Driver = new Driver { DriverId = "D1" }
                });
        }
    }
}
