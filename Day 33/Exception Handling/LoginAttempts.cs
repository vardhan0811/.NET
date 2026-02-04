using System;

// Custom Exception
class LoginAttemptExceededException : Exception
{
    public LoginAttemptExceededException(string message)
        : base(message)
    {
    }
}

class LoginSystem
{
    static void Main()
    {
        int attempts = 0;
        const int MAX_ATTEMPTS = 3;

        string correctPassword = "admin123";

        try
        {
            while (true)
            {
                Console.Write("Enter Password: ");
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Input was null. Please try again.");
                    continue;
                }

                // Check password
                if (input == correctPassword)
                {
                    Console.WriteLine("Login Successful!");
                    break;
                }
                else
                {
                    attempts++;
                    Console.WriteLine("Wrong Password!");

                    // Check attempts limit
                    if (attempts >= MAX_ATTEMPTS)
                    {
                        throw new LoginAttemptExceededException(
                            "Login failed! Maximum attempts exceeded."
                        );
                    }
                }
            }
        }
        catch (LoginAttemptExceededException ex)
        {
            Console.WriteLine("Access Blocked!");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Application Terminated.");
    }
}
