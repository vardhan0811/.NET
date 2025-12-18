using System;
class Day1
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a number:");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
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