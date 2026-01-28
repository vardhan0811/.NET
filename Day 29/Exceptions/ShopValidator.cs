using System;

namespace Day29
{
    public class InvalidGadgetException : Exception
    {
        public InvalidGadgetException(string message) : base(message) { }
    }
    public class GadgetValidatorUtil
    {
        // Validate Gadget ID
        public bool ValidateGadgetID(string gadgetID)
        {
            // Length must be 4
            if (gadgetID.Length != 4)
            {
                throw new InvalidGadgetException("Invalid gadget ID");
            }

            // First character must be uppercase letter
            if (!(gadgetID[0] >= 'A' && gadgetID[0] <= 'Z'))
            {
                throw new InvalidGadgetException("Invalid gadget ID");
            }

            // Next 3 characters must be digits
            for (int i = 1; i <= 3; i++)
            {
                if (!char.IsDigit(gadgetID[i]))
                {
                    throw new InvalidGadgetException("Invalid gadget ID");
                }
            }
            return true;
        }

        // Validate Warranty Period
        public bool ValidateWarrantyPeriod(int period)
        {
            if (period < 6 || period > 36)
            {
                throw new InvalidGadgetException("Invalid warranty period");
            }

            return true;
        }
    }
    public class ShopValidator
    {
        public static void Run()
        {
            GadgetValidatorUtil util = new GadgetValidatorUtil();

            Console.WriteLine("Enter the number of gadget entries");
            int n = int.Parse(Console.ReadLine() ?? "0");

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine("Enter gadget " + i + " details");

                string input = Console.ReadLine() ?? "";

                try
                {
                    // Split input
                    string[] parts = input.Split(':');

                    string gadgetID = parts[0];
                    string gadgetType = parts[1]; // Not validated
                    int warranty = int.Parse(parts[2]);

                    // Validate
                    util.ValidateGadgetID(gadgetID);
                    util.ValidateWarrantyPeriod(warranty);

                    Console.WriteLine("Warranty accepted, stock updated");
                }
                catch (InvalidGadgetException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input format");
                }
            }
        }
    }
}