using System;
using Domain;
using Services;
using Exceptions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementService service = new ManagementService();

            while (true)
            {
                Console.WriteLine("\n1 → Display all medicines");
                Console.WriteLine("2 → Update medicine price");
                Console.WriteLine("3 → Add medicine");
                Console.WriteLine("4 → Exit");
                Console.Write("Enter choice: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine()!);

                    switch (choice)
                    {
                        case 1:
                            {
                                var medicines = service.GetAll();

                                bool found = false;
                                foreach (var med in medicines)
                                {
                                    Console.WriteLine(med);
                                    found = true;
                                }

                                if (!found)
                                    Console.WriteLine("No Medicines Available");

                                break;
                            }

                        case 2:
                            {
                                Console.Write("Enter Medicine Id: ");
                                string id = Console.ReadLine()!;

                                Console.Write("Enter New Price: ");
                                int price = int.Parse(Console.ReadLine()!);

                                service.UpdatePrice(id, price);
                                Console.WriteLine("Price Updated Successfully");
                                break;
                            }

                        case 3:
                            {
                                Console.WriteLine("Enter Medicine Details:");
                                Console.Write("Id: ");
                                string id = Console.ReadLine()!;

                                Console.Write("Name: ");
                                string name = Console.ReadLine()!;

                                Console.Write("Price: ");
                                int price = int.Parse(Console.ReadLine()!);

                                Console.Write("Expiry Year: ");
                                int year = int.Parse(Console.ReadLine()!);

                                Medicine med = new Medicine(id, name, price, year);
                                service.AddMedicine(med);

                                Console.WriteLine("Medicine Added Successfully");
                                break;
                            }

                        case 4:
                            {
                                Console.WriteLine("Thank You!");
                                return;
                            }

                        default:
                            {
                                Console.WriteLine("Invalid Choice");
                                break;
                            }
                    }
                }
                catch (ScenarioException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input Format");
                }
                catch (Exception)
                {
                    Console.WriteLine("Unexpected Error Occurred");
                }
            }
        }
    }
}
