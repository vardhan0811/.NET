using System;

namespace Day20techstack
{
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string message) : base(message) { }
    }

    public class Person
    {
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
    }

    public class MobileNumber
    {
        public static Person ValidatePhoneNumber(string name, string mobileNumber)
        {
            if (mobileNumber.Length != 10 || !long.TryParse(mobileNumber, out _))
            {
                throw new InvalidPhoneNumberException("Invalid mobile number. It must be exactly 10 digits.");
            }

            return new Person
            {
                Name = name,
                MobileNumber = mobileNumber
            };
        }

        public static void Run()
        {
            try
            {
                Console.WriteLine("Enter Name: ");
                string name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Enter Mobile Number: ");
                string mobileNumber = Console.ReadLine() ?? string.Empty;

                Person person = MobileNumber.ValidatePhoneNumber(name, mobileNumber);
                Console.WriteLine("Mobile number validated successfully!");
            }
            catch (InvalidPhoneNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}