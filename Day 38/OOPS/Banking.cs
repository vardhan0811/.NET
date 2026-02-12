using System;

namespace Day38
{
    public class BankAccount
    {
        private int balance;

        public BankAccount(int initialBalance)
        {
            if(initialBalance >= 0)
            {
                balance = initialBalance;
            }
            else
            {
                balance = 0;
            }
        }

        public int GetBalance()
        {
            return balance;
        }

        public void Deposit(int amount)
        {
            if (amount > 0)
            {
                balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
            }
        }
    }

    public class Banking
    {
        public static void Run()
        {
            BankAccount account = new BankAccount(1000);
            for(int i=0;i<=5;i++)
            {
                Console.WriteLine("Enter transaction type(D/W): ");
                string? type = Console.ReadLine();

                Console.WriteLine("Enter amount: ");
                string? input = Console.ReadLine();
                int amount;
                if (!int.TryParse(input, out amount))
                {
                    Console.WriteLine("Invalid amount entered. Transaction skipped.");
                    continue;
                }
                if(type == "D")
                {
                    account.Deposit(amount);
                }
                else if(type == "W")
                {
                    account.Withdraw(amount);
                }
            }

            Console.WriteLine($"Final account balance: {account.GetBalance()}");
        }
    }
}