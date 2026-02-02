using System;

namespace Predicates
{
class Predicates
{
    public static void Run()
    {
        // Predicate
        Predicate<int> isPositive = n => n > 0;

        // Action
        Action<string> show = msg => Console.WriteLine(msg);

        // Func
        Func<int, int, int> multiply = (a, b) => a * b;


        show("Check Number");

        Console.WriteLine(isPositive(10));  // True
        Console.WriteLine(isPositive(-5));  // False

        int result = multiply(4, 5);

        Console.WriteLine("Result: " + result); // 20
    }
}
}