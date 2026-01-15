using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20techstack
{
    public class FindItems
    {
        public static SortedDictionary<string, long> itemDetails = new SortedDictionary<string, long>()
        {
            {"Item1", 100},
            {"Item2", 200},
            {"Item3", 300}
        };

        public static SortedDictionary<string, long> FindItemDetails(long soldCount)
        {
            SortedDictionary<string, long> result = new SortedDictionary<string, long>();

            foreach (var item in itemDetails)
            {
                if (item.Value == soldCount)
                {
                    result.Add(item.Key, item.Value);
                }
            }
            return result;
        }

        public static List<string> FindMinMaxSoldItems()
        {
            var minItem = itemDetails.OrderBy(item => item.Value).FirstOrDefault();
            var maxItem = itemDetails.OrderByDescending(item => item.Value).FirstOrDefault();

            return new List<string>
            {
                $"Minimum Sold Item: {minItem.Key} ({minItem.Value})",
                $"Maximum Sold Item: {maxItem.Key} ({maxItem.Value})"
            };
        }

        public static Dictionary<string, long> SortByCount()
        {
            return itemDetails.OrderBy(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        }

        public static void Run()
        {
            Console.WriteLine("Enter SoldCount: ");
            string? input = Console.ReadLine();
            long soldCount;
            if (!long.TryParse(input, out soldCount))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }
            
            var foundItems = FindItemDetails(soldCount);
            if(foundItems.Count == 0)
            {
                Console.WriteLine("No items found with the specified sold count.");
            }
            else
            {
                foreach (var item in foundItems)
                {
                    Console.WriteLine($"Item Found: {item.Key} ({item.Value})");
                }
            }

            Console.WriteLine("\nMinimum and Maximum Sold Items: ");
            var minMaxItems = FindMinMaxSoldItems();
            Console.WriteLine(minMaxItems[0]);
            Console.WriteLine(minMaxItems[1]);

            Console.WriteLine("\nItems sorted by sold count: ");
            var sortedItems = SortByCount();
            foreach (var item in sortedItems)
            {
                Console.WriteLine($"Item: {item.Key} ({item.Value})");
            }
        }
    }
}
