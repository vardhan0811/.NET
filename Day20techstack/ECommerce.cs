using System;

namespace Day20techstack
{
    /// <summary>
    /// Custom exception thrown when the wallet balance is insufficient for a purchase.
    /// </summary>
    public class InsufficientWalletBalanceException : Exception
    {
        public InsufficientWalletBalanceException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Represents a user's e-commerce shop account with wallet and purchase details.
    /// </summary>
    public class ECommerceShop
    {
        public string? UserName { get; set; }
        public double WalletBalance { get; set; }
        public double TotalPurchaseAmount { get; set; }
    }

    /// <summary>
    /// Handles e-commerce payment logic and wallet balance validation.
    /// </summary>
    public class ECommerce
    {
        /// <summary>
        /// Makes a payment if the wallet balance is sufficient, otherwise throws an exception.
        /// </summary>
        /// <param name="name">User's name</param>
        /// <param name="balance">Current wallet balance</param>
        /// <param name="amount">Purchase amount</param>
        /// <returns>ECommerceShop object with updated balance and purchase info</returns>
        public static ECommerceShop MakePayment(string name, double balance, double amount)
        {
            // Check if the wallet balance is enough for the purchase
            if (balance < amount)
            {
                throw new InsufficientWalletBalanceException(
                    "Insufficient balance in your digital wallet");
            }

            // Create and return a new ECommerceShop object with updated balance
            ECommerceShop shop = new ECommerceShop
            {
                UserName = name,
                WalletBalance = balance - amount,
                TotalPurchaseAmount = amount
            };

            return shop;
        }

        /// <summary>
        /// Runs the payment process by taking user input and validating the transaction.
        /// </summary>
        static void Run(string[] args)
        {
            try
            {
                Console.WriteLine("Enter User Name:");
                string? inputName = Console.ReadLine();
                string name = inputName ?? string.Empty;

                Console.WriteLine("Enter Wallet Balance:");
                string? balanceInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(balanceInput))
                {
                    throw new ArgumentException("Wallet Balance input cannot be empty.");
                }
                double balance = double.Parse(balanceInput);

                Console.WriteLine("Enter Purchase Amount:");
                string? amountInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(amountInput))
                {
                    throw new ArgumentException("Purchase Amount input cannot be empty.");
                }
                double amount = double.Parse(amountInput);

                // Attempt to make the payment
                ECommerceShop result = MakePayment(name, balance, amount);
                Console.WriteLine("Payment successful");
            }
            catch (InsufficientWalletBalanceException ex)
            {
                // Handle custom exception for insufficient balance
                Console.WriteLine(ex.Message);
            }
        }
    }
}
