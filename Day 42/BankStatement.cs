using System;
using System.Collections.Generic;

namespace Day42
{
    public class BankStatement
    {
        public static void Run()
        {
            Console.WriteLine("===== BANK STATEMENT ANALYZER =====");
            Console.WriteLine("Enter transactions.");
            Console.WriteLine("NOTE:");
            Console.WriteLine("- Negative amount = Spending");
            Console.WriteLine("- Positive amount = Income (ignored)");
            Console.WriteLine();

            Dictionary<string, decimal> categoryTotals = new Dictionary<string, decimal>();

            Console.Write("Enter number of transactions: ");
            int transactionCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < transactionCount; i++)
            {
                Console.WriteLine($"\nTransaction {i}:");

                Console.Write("Enter Category: ");
                string category = Console.ReadLine();

                Console.Write("Enter Amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                if (amount < 0)
                {
                    decimal spend = Math.Abs(amount);

                    if (categoryTotals.ContainsKey(category))
                    {
                        categoryTotals[category] += spend;
                    }
                    else
                    {
                        categoryTotals[category] = spend;
                    }
                }
            }

            Console.WriteLine("\nTotal Spending by Category:");
            foreach (var entry in categoryTotals)
            {
                Console.WriteLine($"{entry.Key}: ${entry.Value:F2}");
            }
        }
    }
}