using System;

namespace Day29
{
    class Password
    {
        static void Run()
        {
            Console.WriteLine("Enter the username");
            string username = Console.ReadLine() ?? "";

            // Validate
            if (!IsValid(username))
            {
                Console.WriteLine(username + " is an invalid username");
                return;
            }

            // Generate password
            string password = GeneratePassword(username);

            Console.WriteLine("Password: " + password);
        }

        // Validation method
        static bool IsValid(string u)
        {
            // Rule 1: Length
            if (u.Length != 8)
                return false;

            // Rule 2: First 4 uppercase
            for (int i = 0; i < 4; i++)
            {
                if (!(u[i] >= 'A' && u[i] <= 'Z'))
                    return false;
            }

            // Rule 3: 5th char = '@'
            if (u[4] != '@')
                return false;

            // Rule 4: Last 3 digits
            string last3 = u.Substring(5, 3);

            foreach (char c in last3)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            int courseId = int.Parse(last3);

            if (courseId < 101 || courseId > 115)
                return false;

            return true;
        }

        // Generate password
        static string GeneratePassword(string u)
        {
            int sum = 0;

            // First 4 letters → lowercase → ASCII
            for (int i = 0; i < 4; i++)
            {
                char lower = char.ToLower(u[i]);
                sum += (int)lower;
            }

            // Get last 2 digits
            string last2 = u.Substring(6, 2);

            return "TECH_" + sum + last2;
        }
    }
}