using System;

namespace Day33
{
class InputHandler
{
    public static void Run()
    {
        int number;
        bool isValid = false;

        // Keep asking until valid number is entered
        while (!isValid)
        {
            Console.Write("Enter a number: ");
            string? input = Console.ReadLine();

            // Check for null input
            if (input == null)
            {
                Console.WriteLine("Input was null. Please enter a valid number.");
                continue;
            }

            // Try to convert string to int
            if (int.TryParse(input, out number))
            {
                // Conversion successful
                isValid = true;
                Console.WriteLine("You entered: " + number);
            }
            else
            {
                // Conversion failed
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        Console.WriteLine("Program finished.");
    }
}
}