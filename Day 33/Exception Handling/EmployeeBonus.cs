using System;

namespace Day33
{
    class BonusCalculator
    {
        public static void Run()
        {
            int[] salaries = { 5000, 0, 7000 };

            int totalBonus = 10000; // Fixed bonus amount

            // Loop through all salaries
            for (int i = 0; i < salaries.Length; i++)
            {
                try
                {
                    Console.WriteLine($"Processing Employee {i + 1}");

                    // Divide bonus by salary
                    int bonus = totalBonus / salaries[i];

                    Console.WriteLine($"Bonus: {bonus}");
                }
                catch (DivideByZeroException)
                {
                    // Handle division by zero
                    Console.WriteLine("Error: Salary is zero. Cannot calculate bonus.");
                }
            }

            Console.WriteLine("\nProcessing completed.");
        }
    }
}