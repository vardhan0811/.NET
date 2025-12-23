using System;
public class Day1BasicConcepts
{
    // Method to find prime numbers up to a given limit
    public static void Run(string[] args)
    {
        Console.WriteLine("Enter a number:");
        int n = Convert.ToInt32(Console.ReadLine());
        bool[] isPrime = new bool[n + 1];
        for (int i = 2; i <= n; i++)
            isPrime[i] = true;
        for (int p = 2; p * p <= n; p++)
        {
            if (isPrime[p])
            {
                for (int i = p * p; i <= n; i += p)
                    isPrime[i] = false;
            }
        }
        Console.WriteLine($"Prime numbers up to {n}:");
        for (int i = 2; i <= n; i++)
        {
            if (isPrime[i])
                Console.Write(i + " ");
        }
        Console.WriteLine();
    }
}