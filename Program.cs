// See https://aka.ms/new-console-template for more information
using System;
class Program
{
    static void Main(string[] args)
    {
        // prime number code using sieve of eratosthenes
        Console.Write("Enter a number: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        int n = int.Parse(input);
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
        Console.WriteLine($"Prime numbers up to {n}: ");
        for (int i = 2; i <= n; i++)
        {
            if (isPrime[i])
                Console.Write(i + " ");
        }        
    }
}


