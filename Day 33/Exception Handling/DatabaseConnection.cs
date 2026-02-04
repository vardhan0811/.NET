using System;

namespace Day32
{
    class DatabaseConnection
    {
        public static void Run()
        {
            bool isConnected = false;

            try
            {
                // 1. Open connection
                Console.WriteLine("Opening database connection...");
                isConnected = true;

                // 2. Simulate operation
                Console.WriteLine("Performing database operation...");

                // Simulate failure
                throw new Exception("Database operation failed!");

                // This line will never execute
                Console.WriteLine("Operation successful.");
            }
            catch (Exception ex)
            {
                // Handle error
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // 3. Always close connection
                if (isConnected)
                {
                    Console.WriteLine("Closing database connection...");
                    isConnected = false;
                }
            }

            Console.WriteLine("Program finished.");
        }
    }
}