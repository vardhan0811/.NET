using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21Important
{
    /// <summary>
    /// Abstract base class for generating grocery receipts with prices and discounts.
    /// </summary>
    public abstract class GroceryReceiptBase
    {
        /// <summary>
        /// Stores item prices.
        /// </summary>
        protected Dictionary<string, int> Prices = new Dictionary<string, int>();
        /// <summary>
        /// Stores item discounts.
        /// </summary>
        protected Dictionary<string, int> Discounts = new Dictionary<string, int>();   

        /// <summary>
        /// Initializes the base class with prices and discounts.
        /// </summary>
        protected GroceryReceiptBase(
            Dictionary<string, int> prices,
            Dictionary<string, int> discounts)
        {
            Prices = prices;
            Discounts = discounts;
        }

        /// <summary>
        /// Abstract method to generate an invoice for a list of items.
        /// </summary>
        public abstract void GenerateInvoice(List<string> items);
    }

    /// <summary>
    /// Implements invoice generation for grocery receipts.
    /// </summary>
    public class GroceryReceipts : GroceryReceiptBase
    {
        /// <summary>
        /// Initializes the GroceryReceipts class with prices and discounts.
        /// </summary>
        public GroceryReceipts(
            Dictionary<string, int> prices,
            Dictionary<string, int> discounts) : base(prices, discounts)
        {
        }

        /// <summary>
        /// Generates and prints an invoice for the provided list of items.
        /// </summary>
        /// <param name="items">List of item names</param>
        public override void GenerateInvoice(List<string> items)
        {
            // Group items by name and sort alphabetically
            var groupedItems = items
            .GroupBy(i => i)
            .OrderBy(g => g.Key);

            foreach (var item in groupedItems)
            {
                int price = Prices.ContainsKey(item.Key) ? Prices[item.Key] : 0;
                int discount = Discounts.ContainsKey(item.Key) ? Discounts[item.Key] : 0;
                int quantity = item.Count();

                // Print item details
                Console.WriteLine($"Item: {item.Key}, Price: {price}, Discount: {discount}, Quantity: {quantity}");
            }
        }
    }

    /// <summary>
    /// Entry point for generating a grocery receipt.
    /// </summary>
    public class GroceryReceipt
    {
        /// <summary>
        /// Runs the grocery receipt generation process.
        /// </summary>
        public static void Run()
        {
            var prices = new Dictionary<string, int>
            {
                { "Apple", 100 },
                { "Banana", 50 },
                { "Orange", 80 }
            };

            var discounts = new Dictionary<string, int>
            {
                { "Apple", 10 },
                { "Banana", 5 }
            };

            var receipt = new GroceryReceipts(prices, discounts);
            // Generate invoice for a sample list of items
            receipt.GenerateInvoice(new List<string> { "Apple", "Banana", "Apple", "Orange" });
        }
    }
}