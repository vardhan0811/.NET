using System;
using System.Collections.Generic;

namespace Day29
{
    public class CakeOrder
    {
        // Attribute
        private Dictionary<string, double> orderMap =
            new Dictionary<string, double>();

        // Getter
        public Dictionary<string, double> GetOrderMap()
        {
            return orderMap;
        }

        // Setter
        public void SetOrderMap(Dictionary<string, double> map)
        {
            orderMap = map;
        }

        // Add order details
        public void AddOrderDetails(string orderId, double cakeCost)
        {
            orderMap[orderId] = cakeCost;
        }

        // Find orders above specified cost
        public Dictionary<string, double>
            FindOrdersAboveSpecifiedCost(double cakeCost)
        {
            Dictionary<string, double> result =
                new Dictionary<string, double>();

            foreach (KeyValuePair<string, double> entry in orderMap)
            {
                if (entry.Value > cakeCost)
                {
                    result.Add(entry.Key, entry.Value);
                }
            }

            return result;
        }
        
    }

    public class CakeOrderUI
    {
        public static void Run()
        {
            CakeOrder cakeOrder = new CakeOrder();

            Console.WriteLine("Enter number of cake orders to be added");

            int n = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter the cake order details (Order Id: CakeCost)");

            // Read orders
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine() ?? "";

                // Split by :
                string[] parts = input.Split(':');

                string orderId = parts[0];
                double cost = double.Parse(parts[1]);

                // Store
                cakeOrder.AddOrderDetails(orderId, cost);
            }

            // Read search cost
            Console.WriteLine("Enter the cost to search the cake orders");

            double searchCost = double.Parse(Console.ReadLine() ?? "0");

            // Find orders
            Dictionary<string, double> result =
                cakeOrder.FindOrdersAboveSpecifiedCost(searchCost);

            // Display output
            if (result.Count == 0)
            {
                Console.WriteLine("No cake orders found");
            }
            else
            {
                Console.WriteLine("Cake Orders above the specified cost");

                foreach (KeyValuePair<string, double> entry in result)
                {
                    Console.WriteLine("Order ID: " + entry.Key +
                                      ", Cake Cost: " + entry.Value);
                }
            }
        }
    }
}