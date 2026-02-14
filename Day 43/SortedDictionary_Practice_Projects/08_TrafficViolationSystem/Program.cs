using System;
using Domain;
using Services;
using Exceptions;

ManagementService service = new ManagementService();

while (true)
{
    Console.WriteLine("\n1 Display Violations");
    Console.WriteLine("2 Pay Fine");
    Console.WriteLine("3 Add Violation");
    Console.WriteLine("4 Exit");

    Console.Write("Choice: ");
    int choice = int.Parse(Console.ReadLine());

    try
    {
        switch (choice)
        {
            case 1:
                foreach (var v in service.GetAll())
                    Console.WriteLine($"{v.Id} {v.OwnerName} Fine:{v.FineAmount}");
                break;

            case 2:
                Console.Write("Vehicle Number: ");
                string id = Console.ReadLine();
                service.PayFine(id);
                break;

            case 3:
                Violation vio = new Violation();

                Console.Write("Vehicle Number: ");
                vio.Id = Console.ReadLine();

                Console.Write("Owner Name: ");
                vio.OwnerName = Console.ReadLine();

                Console.Write("Fine Amount: ");
                vio.FineAmount = int.Parse(Console.ReadLine());

                service.AddViolation(vio);
                break;

            case 4:
                return;
        }
    }
    catch (CustomBaseException ex)
    {
        Console.WriteLine(ex.Message);
    }
}