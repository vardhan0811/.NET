using System;

namespace Day33
{
    class OrderProcessor
    {
        public static void Run()
        {
            int[] orders = { 101, -1, 103 };

            foreach (int orderId in orders)
            {
                try
                {
                    ProcessOrder(orderId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.WriteLine("All orders processed.");
        }

        public static void ProcessOrder(int orderId)
        {
            // Check for invalid order
            if (orderId <= 0)
            {
                throw new Exception("Invalid Order ID: " + orderId);
            }

            // Simulate order processing
            Console.WriteLine("Processing Order: " + orderId);
        }
    }
}