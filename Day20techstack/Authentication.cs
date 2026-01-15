using System;
namespace Day20techstack
{
    /// <summary>
    /// Custom exception thrown when password and confirmation password do not match.
    /// </summary>
    public class PasswordMismatchException : Exception
    {
        public PasswordMismatchException(string message) : base(message){}
    }

    /// <summary>
    /// Represents a user with name and password information.
    /// </summary>
    public class User
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ConfirmationPassword { get; set; }
    }

    /// <summary>
    /// Handles authentication logic including password validation.
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Validates that the password and confirmation password match.
        /// Throws PasswordMismatchException if they do not match.
        /// </summary>
        /// <param name="name">User's name</param>
        /// <param name="password">Password</param>
        /// <param name="confirmationPassword">Confirmation password</param>
        /// <returns>User object if validation passes</returns>
        public static User ValidatePassword(string name, string password, string confirmationPassword)
        {
            // Check if password and confirmation password are the same
            if (!password.Equals(confirmationPassword))
            {
                throw new PasswordMismatchException("Password and confirmation password do not match.");
            }
            // Create and return a new User object
            User user = new User
            {
                Name = name,
                Password = password,
                ConfirmationPassword = confirmationPassword
            };
            return user;
        }

        /// <summary>
        /// Runs the authentication process by taking user input and validating passwords.
        /// </summary>
        public static void Run()
        {
            try{
                Console.WriteLine("Enter User Name: ");
                string name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Enter Password: ");
                string password = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Confirm Password: ");
                string confirmationPassword = Console.ReadLine() ?? string.Empty;

                // Validate the password and confirmation password
                User user = Authentication.ValidatePassword(name, password, confirmationPassword);
                Console.WriteLine("User registered successfully!");
            }
            catch (PasswordMismatchException ex)
            {
                // Handle custom exception for password mismatch
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}