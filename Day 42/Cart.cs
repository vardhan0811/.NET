using System;
using System.Collections.Generic;

namespace Day42
{
    public class Cart
    {
        public static void Run()
        {
            Dictionary<string, int> sku = new Dictionary<string, int>();
            Console.WriteLine("Enter number of items to add to cart:");
            int itemCount = int.Parse(Console.ReadLine() ?? "0");

            for(int i=0;i<=itemCount;i++)
            {
                Console.WriteLine($"\nItem {i}: ");
                Console.Write("Enter SKU: ");
                string? itemSku = Console.ReadLine();

                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine() ?? "0");

                if(quantity<=0)
                {
                    Console.WriteLine("Ignored (quantity<=0)");
                    continue;
                }

                if(sku.ContainsKey(itemSku))
                {
                    sku[itemSku] += quantity;
                }
                else
                {
                    sku[itemSku] = quantity;
                }
            }

            Console.WriteLine("\nCart Summary:");
            foreach(var item in sku)
            {
                Console.WriteLine($"SKU: {item.Key}, Quantity: {item.Value}");
            }
        }
    }
}