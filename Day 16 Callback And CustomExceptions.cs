using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Day16CallbackAndCustomExceptions
{
    public class Day16CallbackAndCustomExceptions
    {
        public static void Run()
        {
            var service = new OrderService();
            // Pass a method as callback
            service.PlaceOrder("ORD-101", SendEmail);
            // Pass another method as callback
            service.PlaceOrder("ORD-102", SendSms);
            static void SendEmail(string msg) => Console.WriteLine("EMAIL: " + msg);
            static void SendSms(string msg) => Console.WriteLine("SMS:   " + msg);

            // Custom Exception Demo
            try
            {
                int result = CustomException.Divide(10, 0); // This will throw an exception
                Console.WriteLine("Result: " + result);
            }
            catch (AppCustomException ex) // Catching custom exception
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

#region delegateExamples
// 1) Create a delegate type (signature: void (string))
    public delegate void Notify(string message);
    class OrderService
    {
        // 2) Accept a delegate as parameter (callback)
        public void PlaceOrder(string orderId, Notify callback)
        {
            Console.WriteLine($"Order {orderId} placed.");
            // 3) Call the callback (when something important happens)
            callback?.Invoke($"Order {orderId} confirmation sent!");
        }
    }
#endregion delegateExamples

    // Custom Exception
    public class AppCustomException : Exception
    {
        // Custom message for internal exceptions
        public override string Message => HandleBase(base.Message);

        private string HandleBase(string sysMessage)
        {
            // original message from base class
            Console.WriteLine(sysMessage);
            return "Internal Exception Occurred. Please contact Admin.";
        }
    }

    // Custom Exception calling
    public class CustomException
    {
        public static int Divide(int v1, int v2)
        {
            try
            {
                return v1 / v2; // Attempting division
            }
            catch 
            {
                throw new AppCustomException(); // Throwing custom exception
            }
        
        }
    }
}