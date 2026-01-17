using System;
using System.Linq;

namespace Day21Important
{
    /// <summary>
    /// Interface for bank account operations such as deposit, withdraw, and processing messages.
    /// </summary>
    public interface IBankAccountOperation
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        decimal ProcessOperation(string message);
    }

    /// <summary>
    /// Implements bank operations and processes chat messages to perform actions.
    /// </summary>
    public class BankOperations : IBankAccountOperation
    {
        private decimal balance = 0;

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        public void Deposit(decimal d)
        {
            balance += d;
        }

        /// <summary>
        /// Withdraws the specified amount from the account if sufficient balance exists.
        /// </summary>
        public void Withdraw(decimal d)
        {
            if (balance >= d)
            {
                balance -= d;
            }
        }

        /// <summary>
        /// Processes a chat message to determine the requested bank operation.
        /// </summary>
        /// <param name="message">The user's chat message</param>
        /// <returns>The current balance after the operation</returns>
        public decimal ProcessOperation(string message)
        {
            string msg = message.ToLower();
            // If the message is a balance inquiry
            if (msg.Contains("see") || msg.Contains("show"))
            {
                return balance;
            }

            // Extract the first number from the message as the amount
            decimal amount = decimal.Parse(
                msg.Split(' ')
                    .First(word => decimal.TryParse(word, out _))
            );

            // If the message is a deposit or similar action
            if (msg.Contains("deposit") || msg.Contains("put") || msg.Contains("invest") || msg.Contains("transfer"))
            {
                Deposit(amount);
            }
            // If the message is a withdrawal or similar action
            else if (msg.Contains("withdraw") || msg.Contains("pull"))
            {
                Withdraw(amount);
            }
            return balance;
        }
    }

    /// <summary>
    /// Simulates a bank chatbot that processes user messages for banking operations.
    /// </summary>
    public class BankChatBot
    {
        /// <summary>
        /// Runs the chatbot, reading user messages and displaying results.
        /// </summary>
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine());
            IBankAccountOperation bank = new BankOperations();
            for (int i = 0; i < n; i++)
            {
                string message = Console.ReadLine();
                decimal result = bank.ProcessOperation(message);
                Console.WriteLine(result);
            }
        }
    }

}