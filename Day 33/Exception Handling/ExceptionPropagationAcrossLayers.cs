using System;

namespace Day33
{
class Controller
{
    static void Run()
    {
        try
        {
            // Call Service layer
            Service.Process();
        }
        catch (Exception ex)
        {
            // Handle exception here (UI layer)
            Console.WriteLine("Controller: Error handled.");
            Console.WriteLine("Message: " + ex.Message);
        }

        Console.WriteLine("Program Ended.");
    }
}

class Service
{
    public static void Process()
    {
        try
        {
            // Call Repository layer
            Repository.GetData();
        }
        catch (Exception ex)
        {
            // Log error (Service layer)
            Console.WriteLine("Service: Logging error...");
            Console.WriteLine("Service Log: " + ex.Message);

            // Rethrow exception to upper layer
            throw;
        }
    }
}

class Repository
{
    public static void GetData()
    {
        // Simulate error
        throw new Exception("Repository: Database connection failed.");
    }
}
}