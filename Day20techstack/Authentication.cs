using System;
namespace Day20techstack
{
    public class PasswordMismatchException : Exception
    {
        public PasswordMismatchException(string message) : base(message){}
    }

    public class User
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ConfirmationPassword { get; set; }
    }

    public class Authentication
    {
        public static User ValidatePassword(string name, string password, string confirmationPassword)
        {
            if (!password.Equals(confirmationPassword))
            {
                throw new PasswordMismatchException("Password and confirmation password do not match.");
            }
            User user = new User
            {
                Name = name,
                Password = password,
                ConfirmationPassword = confirmationPassword
            };
            return user;
        }

        public static void Run()
        {
            try{
                Console.WriteLine("Enter User Name: ");
                string name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Enter Password: ");
                string password = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Confirm Password: ");
                string confirmationPassword = Console.ReadLine() ?? string.Empty;

                User user = Authentication.ValidatePassword(name, password, confirmationPassword);
                Console.WriteLine("User registered successfully!");
            }
            catch (PasswordMismatchException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}