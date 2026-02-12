using System;

namespace Day41
{
    public static class EmailValidator
    {
        public static bool IsValidGmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Must contain exactly one '@'
            var parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            string localPart = parts[0];
            string domainPart = parts[1];

            // Domain must be exactly gmail.com
            if (!domainPart.Equals("gmail.com", StringComparison.OrdinalIgnoreCase))
                return false;

            // Local part length check (Gmail requirement)
            if (localPart.Length < 6 || localPart.Length > 30)
                return false;

            // Cannot start or end with dot
            if (localPart.StartsWith(".") || localPart.EndsWith("."))
                return false;

            // Cannot contain consecutive dots
            if (localPart.Contains(".."))
                return false;

            // Only letters, digits, and dot allowed
            foreach (char c in localPart)
            {
                if (!(char.IsLetterOrDigit(c) || c == '.'))
                    return false;
            }

            return true;
        }
    }

    public class EmailValidation
    {
        public static void Run(string[] args)
        {
            Console.WriteLine("Enter an email address: ");
            string email = Console.ReadLine() ?? string.Empty;

            if (EmailValidator.IsValidGmail(email))
            {
                Console.WriteLine("The email address is a valid Gmail address.");
            }
            else
            {
                Console.WriteLine("The email address is not a valid Gmail address.");
            }
        }
    }
}
