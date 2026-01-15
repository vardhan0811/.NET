using System;

namespace Day20techstack
{
    /// <summary>
    /// Custom exception thrown when a mobile number is invalid.
    /// </summary>
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string message) : base(message) { }
    }

    /// <summary>
    /// Represents a person with a name and mobile number.
    /// </summary>
    public class Person
    {
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
    }

    /// <summary>
    /// Provides methods to validate and process mobile numbers.
    /// </summary>
    public class MobileNumber
    {
        /// <summary>
        /// Validates that the mobile number is exactly 10 digits.
        /// Throws InvalidPhoneNumberException if validation fails.
        /// </summary>
        /// <param name="name">Person's name</param>
        /// <param name="mobileNumber">Mobile number to validate</param>
        /// <returns>Person object if validation passes</returns>
        public static Person ValidatePhoneNumber(string name, string mobileNumber)
        {
            // Check if the mobile number is exactly 10 digits and numeric
            if (mobileNumber.Length != 10 || !long.TryParse(mobileNumber, out _))
            {
                throw new InvalidPhoneNumberException("Invalid mobile number. It must be exactly 10 digits.");
            }

            // Return a new Person object if validation passes
            return new Person
            {
                Name = name,
                MobileNumber = mobileNumber
            };
        }

        /// <summary>
        /// Runs the mobile number validation process by taking user input.
        /// </summary>
        public static void Run()
        {
            try
            {
                Console.WriteLine("Enter Name: ");
                string name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Enter Mobile Number: ");
                string mobileNumber = Console.ReadLine() ?? string.Empty;

                // Validate the mobile number
                Person person = MobileNumber.ValidatePhoneNumber(name, mobileNumber);
                Console.WriteLine("Mobile number validated successfully!");
            }
            catch (InvalidPhoneNumberException ex)
            {
                // Handle custom exception for invalid phone number
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}