using System;
using System.Collections.Generic;

namespace Day29
{
    public class Apartment
    {
        // Attribute
        private Dictionary<string, double> apartmentDetailsMap = new Dictionary<string, double>();

        // Add apartment details
        public void AddApartmentDetails(string apartmentNumber, double rent)
        {
            apartmentDetailsMap[apartmentNumber] = rent;
        }

        // Find total rent in range
        public double FindTotalRentOfApartmentsInTheGivenRange(double minimumRent, double maximumRent)
        {
            double total = 0;

            foreach (KeyValuePair<string, double> entry in apartmentDetailsMap)
            {
                double rent = entry.Value;

                // Check if rent is in range (inclusive)
                if (rent >= minimumRent && rent <= maximumRent)
                {
                    total += rent;
                }
            }

            return total;
        }
    }

    public class HeavenHomes
    {
        public static void Run()
        {
            Apartment apartment = new Apartment();

            // Step 1: Get number of apartments
            Console.WriteLine("Enter number of details to be added");
            int n = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter the details (Apartment number: Rent)");

            // Step 2: Read apartment details
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine() ?? "";

                // Split by :
                string[] parts = input.Split(':');

                string apartmentNo = parts[0];
                double rent = double.Parse(parts[1]);

                // Store details
                apartment.AddApartmentDetails(apartmentNo, rent);
            }

            // Step 3: Read range
            Console.WriteLine("Enter the range to filter the details");

            double minRent = double.Parse(Console.ReadLine() ?? "0");
            double maxRent = double.Parse(Console.ReadLine() ?? "0");

            // Step 4: Find total
            double total = apartment.FindTotalRentOfApartmentsInTheGivenRange(minRent, maxRent);

            // Step 5: Display result
            if (total == 0)
            {
                Console.WriteLine("No apartments found in this range");
            }
            else
            {
                Console.WriteLine("Total Rent in the range " + minRent + " to " + maxRent + " USD:" + total);
            }
        }
    }
}