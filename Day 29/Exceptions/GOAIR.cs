using System;

namespace Day29
{
    public class InvalidEntryException : Exception
    {
        public InvalidEntryException(string message) : base(message) { }
    }

    public class EntryUtility
    {
        // Validate Employee ID
        public bool ValidateEmployeeId(string employeeId)
        {
            // Length must be 10
            if (employeeId.Length != 10)
            {
                throw new InvalidEntryException("Invalid Employee Id");
            }

            // Must start with "GOAIR/"
            if (!employeeId.StartsWith("GOAIR/"))
            {
                throw new InvalidEntryException("Invalid Employee Id");
            }

            // Last 4 must be digits
            string last4 = employeeId.Substring(6, 4);

            foreach (char c in last4)
            {
                if (!char.IsDigit(c))
                {
                    throw new InvalidEntryException("Invalid Employee Id");
                }
            }

            return true;
        }

        // Validate Duration
        public bool ValidateDuration(int duration)
        {
            if (duration < 1 || duration > 5)
            {
                throw new InvalidEntryException("Invalid Duration");
            }

            return true;
        }
    }
    class GOAIR
    {
        static void Run()
        {
            EntryUtility utility = new EntryUtility();
    
            Console.WriteLine("Enter the number of entries");
            int n = int.Parse(Console.ReadLine() ?? "0");
    
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine("Enter entry " + i + " details");
    
                string input = Console.ReadLine() ?? "";
    
                try
                {
                    // Split input
                    string[] parts = input.Split(':');
                    string employeeId = parts[0];
                    string entryType = parts[1];   // Not validated
                    int duration = int.Parse(parts[2]);
    
                    // Validate
                    utility.ValidateEmployeeId(employeeId);
                    utility.ValidateDuration(duration);
    
                    Console.WriteLine("Valid entry details");
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid entry details");
                }
            }
        }
    }
}