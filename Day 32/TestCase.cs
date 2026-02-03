using NUnit.Framework;
using System;

namespace Day32
{
    // Test fixture for bank account operations
    [TestFixture]
    public class UnitTest
    {
        // Test depositing a valid amount increases the balance
        [Test]
        public void Test_Deposit_ValidAmount()
        {
            var account = new Program(100m);
            account.Deposit(50m);
            Assert.AreEqual(150m, account.Balance);
        }

        // Test depositing a negative amount throws an exception
        [Test]
        public void Test_Deposit_NegativeAmount()
        {
            var account = new Program(100m);
            var ex = Assert.Throws<Exception>(() => account.Deposit(-10m));
            Assert.AreEqual("Deposit amount cannot be negative", ex.Message);
        }

        // Test withdrawing a valid amount decreases the balance
        [Test]
        public void Test_Withdraw_ValidAmount()
        {
            var account = new Program(200m);
            account.Withdraw(50m);
            Assert.AreEqual(150m, account.Balance);
        }

        // Test withdrawing more than the balance throws an exception
        [Test]
        public void Test_Withdraw_InsufficientFunds()
        {
            var account = new Program(100m);
            var ex = Assert.Throws<Exception>(() => account.Withdraw(200m));
            Assert.AreEqual("Insufficient funds.", ex.Message);
        }
    }
}