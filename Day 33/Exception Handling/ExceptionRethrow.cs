using System;

namespace Day33
{
    class ExceptionRethrow
    {
        public static void Run()
        {
            try
            {
                ProcessData();
            }
            catch (Exception ex)
            {
                // Final handling here
                Console.WriteLine("Final Exception Caught in Main");
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void ProcessData()
        {
            try
            {
                // This will fail
                int.Parse("ABC");
            }
            catch (Exception ex)
            {
                // Log exception (local handling)
                Console.WriteLine("Error in ProcessData:");
                Console.WriteLine(ex.Message);

                // Rethrow correctly (preserve stack trace)
                throw;
            }
        }
    }
}