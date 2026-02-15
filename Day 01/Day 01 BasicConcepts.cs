using System;

/// <summary>
/// Demonstrates basic concepts: finding prime numbers up to a given limit.
/// </summary>
public class Day01BasicConcepts
{
    /// <summary>
    /// Finds and prints all prime numbers up to a user-specified limit.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    public static void Run(string[] args)
    {
        // Prompt the user to enter a number
        Console.WriteLine("Enter a number:");
        // Read the number from the console
        int n = Convert.ToInt32(Console.ReadLine());
        // Create a boolean array to mark prime numbers
        bool[] isPrime = new bool[n + 1];
        // Initialize all entries as true (potential primes)
        for (int i = 2; i <= n; i++)
            isPrime[i] = true;
        // Sieve of Eratosthenes algorithm to mark non-primes
        for (int p = 2; p * p <= n; p++)
        {
            // If p is still marked as prime
            if (isPrime[p])
            {
                // Mark all multiples of p as non-prime
                for (int i = p * p; i <= n; i += p)
                    isPrime[i] = false;
            }
        }
        // Print all prime numbers up to n
        Console.WriteLine($"Prime numbers up to {n}:");
        for (int i = 2; i <= n; i++)
        {
            // If i is prime, print it
            if (isPrime[i])
                Console.Write(i + " ");
        }
        // Print a newline at the end
        Console.WriteLine();
    }
}