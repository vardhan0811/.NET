using System;

namespace Day38
{
    // Base class
    public class Cab
    {
        public virtual int CalculateFare(int km)
        {
            // Default implementation (could be abstract if desired)
            return 0;
        }
    }

    // Mini cab
    public class Mini : Cab
    {
        public override int CalculateFare(int km)
        {
            return km * 12;
        }
    }

    // Sedan cab
    public class Sedan : Cab
    {
        public override int CalculateFare(int km)
        {
            return km * 15 + 50;
        }
    }

    // SUV cab
    public class SUV : Cab
    {
        public override int CalculateFare(int km)
        {
            return km * 18 + 100;
        }
    }

    class CabBooking
    {
        static void Run(string[] args)
        {
            Console.WriteLine("Enter cab type (Mini/Sedan/SUV):");
            string? cabTypeInput = Console.ReadLine();
            if (cabTypeInput == null)
            {
                Console.WriteLine("No input provided for cab type.");
                return;
            }
            string cabType = cabTypeInput.Trim();
            Console.WriteLine("Enter distance in km:");
            string? kmInput = Console.ReadLine();
            if (kmInput == null)
            {
                Console.WriteLine("No input provided for distance.");
                return;
            }
            int km = int.Parse(kmInput);

            Cab cab;
            switch (cabType.ToLower())
            {
                case "mini":
                    cab = new Mini();
                    break;
                case "sedan":
                    cab = new Sedan();
                    break;
                case "suv":
                    cab = new SUV();
                    break;
                default:
                    Console.WriteLine("Invalid cab type.");
                    return;
            }

            int fare = cab.CalculateFare(km);
            Console.WriteLine($"Total fare for {cabType} ({km} km): {fare}");
        }
    }
}