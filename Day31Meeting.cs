using System;
using System.Collections.Generic;

namespace Day31Meeting
{
    class Bike
    {
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public int PricePerDay { get; set; }
        public Bike(string model, string brand, int price){
            Model = model;
            Brand = brand;
            PricePerDay = price;
        }
    }

    class BikeUtility
    {
        public void AddBikeDetails(string model, string brand, int pricePerDay)
        {
            int key = Day31Meeting.bikeDetails.Count + 1;
            Bike bike = new Bike(model, brand, pricePerDay);
            Day31Meeting.bikeDetails.Add(key, bike);
        }

        public SortedDictionary<string, List<Bike>> GetBikesGroupedByBrand()
        {
            SortedDictionary<string, List<Bike>> bikesByBrand = new SortedDictionary<string, List<Bike>>();

            foreach (var bike in Day31Meeting.bikeDetails.Values)
            {
                if (string.IsNullOrEmpty(bike.Brand))
                {
                    continue; 
                }
                if (!bikesByBrand.ContainsKey(bike.Brand))
                {
                    bikesByBrand[bike.Brand] = new List<Bike>();
                }
                bikesByBrand[bike.Brand].Add(bike);
            }

            return bikesByBrand;
        }
    }

    class Day31Meeting
    {
        public static SortedDictionary<int, Bike> bikeDetails = new SortedDictionary<int, Bike>();

        public static void Run()
        {
            BikeUtility utility = new BikeUtility();
            while(true)
            {
                Console.WriteLine("1. Add Bike Details");
                Console.WriteLine("2. Display Bikes Grouped by Brand");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine() ?? "0");
                switch(choice)
                {
                    case 1:
                        Console.Write("Enter Bike Model: ");
                        string model = Console.ReadLine() ?? string.Empty;
                        Console.Write("Enter Bike Brand: ");
                        string brand = Console.ReadLine() ?? string.Empty;
                        Console.Write("Enter Price Per Day: ");
                        int pricePerDay = int.Parse(Console.ReadLine() ?? "0");
                        utility.AddBikeDetails(model, brand, pricePerDay);
                        Console.WriteLine("Bike details added successfully.\n");
                        break;
                    case 2:
                        var bikesByBrand = utility.GetBikesGroupedByBrand();
                        foreach (var brandGroup in bikesByBrand)
                        {
                            Console.WriteLine($"\nBrand: {brandGroup.Key}");
                            foreach (var bike in brandGroup.Value)
                            {
                                Console.WriteLine($"Model: {bike.Model}, Price Per Day: {bike.PricePerDay}");
                            }
                        }
                        Console.WriteLine();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }
    }
}