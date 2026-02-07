using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q09_ParallelAggregation;
using M1.Practice.Domain.Q09_ParallelAggregation;

namespace M1.Practice.Tests.Q09_ParallelAggregation
{
    [TestClass]
    public class SalesAggregatorTests
    {
        [TestMethod]
        public void AggregateByRegion_ShouldWork()
        {
            var sales = new List<Sale>
            {
                new Sale
                {
                    Region="East",
                    Category="A",
                    Amount=100,
                    Date=DateTime.Today
                },
                new Sale
                {
                    Region="East",
                    Category="B",
                    Amount=200,
                    Date=DateTime.Today
                },
                new Sale
                {
                    Region="West",
                    Category="A",
                    Amount=300,
                    Date=DateTime.Today
                },
                new Sale
                {
                    Region="West",
                    Category="A",
                    Amount=100,
                    Date=DateTime.Today
                }
            };

            var agg = new SalesAggregator();

            var result =
                agg.AggregateByRegion(sales);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("East", result[0].Region);
            Assert.AreEqual(300, result[0].TotalSales);
            Assert.AreEqual("B", result[0].TopCategory);

            Assert.AreEqual("West", result[1].Region);
            Assert.AreEqual(400, result[1].TotalSales);
            Assert.AreEqual("A", result[1].TopCategory);
        }

        [TestMethod]
        public void BestDay_ShouldReturnHighest()
        {
            var sales = new List<Sale>
            {
                new Sale
                {
                    Date=DateTime.Today,
                    Amount=100
                },
                new Sale
                {
                    Date=DateTime.Today,
                    Amount=200
                },
                new Sale
                {
                    Date=DateTime.Today.AddDays(-1),
                    Amount=500
                }
            };

            var agg = new SalesAggregator();

            var best =
                agg.GetBestSalesDay(sales);

            Assert.AreEqual(
                DateTime.Today.AddDays(-1).Date,
                best.Date);

            Assert.AreEqual(500, best.TotalSales);
        }
    }
}
