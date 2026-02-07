using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q06_MoneyTransfer;
using M1.Practice.Domain.Q06_MoneyTransfer;

namespace M1.Practice.Tests.Q06_MoneyTransfer
{
    [TestClass]
    public class MoneyTransferTests
    {
        [TestMethod]
        public void Transfer_WhenValid_ShouldUpdateBalances()
        {
            var accounts =
                new Dictionary<string, Account>
                {
                    ["A1"] = new Account
                    {
                        AccountNo = "A1",
                        Balance = 1000
                    },
                    ["A2"] = new Account
                    {
                        AccountNo = "A2",
                        Balance = 500
                    }
                };

            var service =
                new MoneyTransferService(accounts);

            var result =
                service.Transfer("A1", "A2", 200);

            if (result.IsSuccess)
            {
                Assert.AreEqual(
                    800,
                    accounts["A1"].Balance);

                Assert.AreEqual(
                    700,
                    accounts["A2"].Balance);
            }
        }

        [TestMethod]
        public void Transfer_WhenInsufficient_ShouldThrow()
        {
            var accounts =
                new Dictionary<string, Account>
                {
                    ["A1"] = new Account
                    {
                        AccountNo = "A1",
                        Balance = 100
                    },
                    ["A2"] = new Account
                    {
                        AccountNo = "A2",
                        Balance = 500
                    }
                };

            var service =
                new MoneyTransferService(accounts);

            try
            {
                service.Transfer("A1", "A2", 500);
                Assert.Fail("Expected InvalidTransferException");
            }
            catch (InvalidTransferException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
