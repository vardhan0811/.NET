using System;

namespace Day33
{
    class BankAccount
    {
        public static void Run()
        {
            int balance = 10000; // Initial account balance

            Console.WriteLine("Current balance: " + balance);
            Console.WriteLine("Enter withdrawal amount:");
            string? input = Console.ReadLine();

            // Validate input: check for null/empty and parse to integer
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int amount))
                throw new ArgumentException("Invalid input. Please enter a valid number.");

            try
            {
                // Check if withdrawal amount is positive
                if (amount <= 0)
                    throw new ArgumentException("Withdrawal amount must be greater than zero.");

                // Check if balance is sufficient
                if (amount > balance)
                    throw new InvalidOperationException("Insufficient balance.");

                // Deduct amount from balance
                balance -= amount;
                Console.WriteLine($"Withdrawal successful. Remaining balance: {balance}");
            }
            catch (Exception ex)
            {
                // Handle any exceptions and display error message
                Console.WriteLine($"Transaction failed: {ex.Message}");
            }
            finally
            {
                // Always execute this block
                Console.WriteLine("Transaction logged.");
                Console.WriteLine("Thank you for using our banking service.");
            }            
        }
    }
}