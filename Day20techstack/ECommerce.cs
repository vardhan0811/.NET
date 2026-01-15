using System;

namespace Day20techstack
{
    public class InsufficientWalletBalanceException : Exception
    {
        public InsufficientWalletBalanceException(string message) : base(message)
        {
        }
    }

    public class ECommerceShop
    {
        public string? UserName { get; set; }
        public double WalletBalance { get; set; }
        public double TotalPurchaseAmount { get; set; }
    }

    public class ECommerce
    {
        public static ECommerceShop MakePayment(string name, double balance, double amount)
        {
            if (balance < amount)
            {
                throw new InsufficientWalletBalanceException(
                    "Insufficient balance in your digital wallet");
            }

            ECommerceShop shop = new ECommerceShop
            {
                UserName = name,
                WalletBalance = balance - amount,
                TotalPurchaseAmount = amount
            };

            return shop;
        }

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

                ECommerceShop result = MakePayment(name, balance, amount);
                Console.WriteLine("Payment successful");
            }
            catch (InsufficientWalletBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
