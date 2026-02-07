using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q04_ProducerConsumer;
using M1.Practice.Domain.Q04_ProducerConsumer;

namespace M1.Practice.Tests.Q04_ProducerConsumer
{
    [TestClass]
    public class ProducerConsumerTests
    {
        [TestMethod]
        public async Task AllOrders_ShouldBeProcessed()
        {
            var orders = new List<Order>();

            for (int i = 1; i <= 10; i++)
            {
                orders.Add(new Order
                {
                    OrderId = i,
                    Product = "Item" + i
                });
            }

            var processor = new OrderProcessor();

            var result =
                await processor.ProcessAsync(orders);

            Assert.AreEqual(10, result);
        }
    }
}
