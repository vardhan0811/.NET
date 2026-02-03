using System;

namespace Day33
{
    class BankAccount
    {
        public static void Run()
        {
            int balance = 10000;

            Console.WriteLine("Current balance: " + balance);
            Console.WriteLine("Enter withdrawal amount:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int amount))
                throw new ArgumentException("Invalid input. Please enter a valid number.");

            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Withdrawal amount must be greater than zero.");

                if (amount > balance)
                    throw new InvalidOperationException("Insufficient balance.");

                balance -= amount;
                Console.WriteLine($"Withdrawal successful. Remaining balance: {balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transaction failed: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Transaction logged.");
                Console.WriteLine("Thank you for using our banking service.");
            }            
        }
    }
}