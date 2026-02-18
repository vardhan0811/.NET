using System;
using System.Collections.Generic;

public class PredicateGenericList
{
    public static void Main()
    {
        var nums = new List<int> { 2, 5, 8, 11, 14 };

        var evens = Filter(nums, n => n % 2 == 0);
        Console.WriteLine(string.Join(",", evens));         // Expected: 2,8,14

        var big = Filter(nums, n => n >= 10);
        Console.WriteLine(string.Join(",", big));           // Expected: 11,14
    }

    // ✅ TODO: Students implement only this function
    public static List<T> Filter<T>(List<T> items, Predicate<T> match)
    {
        // TODO: return a new list with matched items
        var result = items.Where(item => match(item)).ToList();
        return result.Count==0 ? default! : result;
    }
}