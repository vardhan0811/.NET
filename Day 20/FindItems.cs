using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20techstack
{
    /// <summary>
    /// Provides methods to find, sort, and analyze item sales data.
    /// </summary>
    public class FindItems
    {
        /// <summary>
        /// Stores item names and their sold counts.
        /// </summary>
        public static SortedDictionary<string, long> itemDetails = new SortedDictionary<string, long>()
        {
            {"Item1", 100},
            {"Item2", 200},
            {"Item3", 300}
        };

        /// <summary>
        /// Finds items with a specific sold count.
        /// </summary>
        /// <param name="soldCount">The sold count to search for</param>
        /// <returns>Dictionary of items with the specified sold count</returns>
        public static SortedDictionary<string, long> FindItemDetails(long soldCount)
        {
            SortedDictionary<string, long> result = new SortedDictionary<string, long>();

            // Search for items with the given sold count
            foreach (var item in itemDetails)
            {
                if (item.Value == soldCount)
                {
                    result.Add(item.Key, item.Value);
                }
            }
            return result;
        }

        /// <summary>
        /// Finds the items with the minimum and maximum sold counts.
        /// </summary>
        /// <returns>List with descriptions of min and max sold items</returns>
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

        /// <summary>
        /// Returns all items sorted by their sold count in ascending order.
        /// </summary>
        /// <returns>Dictionary of items sorted by sold count</returns>
        public static Dictionary<string, long> SortByCount()
        {
            return itemDetails.OrderBy(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        }

        /// <summary>
        /// Runs the item search and analysis process by taking user input.
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("Enter SoldCount: ");
            string? input = Console.ReadLine();
            long soldCount;
            // Validate user input for sold count
            if (!long.TryParse(input, out soldCount))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }
            
            // Find and display items with the specified sold count
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

            // Display minimum and maximum sold items
            Console.WriteLine("\nMinimum and Maximum Sold Items: ");
            var minMaxItems = FindMinMaxSoldItems();
            Console.WriteLine(minMaxItems[0]);
            Console.WriteLine(minMaxItems[1]);

            // Display all items sorted by sold count
            Console.WriteLine("\nItems sorted by sold count: ");
            var sortedItems = SortByCount();
            foreach (var item in sortedItems)
            {
                Console.WriteLine($"Item: {item.Key} ({item.Value})");
            }
        }
    }
}
