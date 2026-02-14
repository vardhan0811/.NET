using System;
using Domain;
using Services;
using Exceptions;

ManagementService service = new ManagementService();

while (true)
{
    Console.WriteLine("\n1 Display Investments");
    Console.WriteLine("2 Update Risk");
    Console.WriteLine("3 Add Investment");
    Console.WriteLine("4 Exit");

    Console.Write("Choice: ");
    int choice = int.Parse(Console.ReadLine());

    try
    {
        switch (choice)
        {
            case 1:
                foreach (var i in service.GetAll())
                    Console.WriteLine($"{i.Id} {i.AssetName} Risk:{i.RiskRating}");
                break;

            case 2:
                Console.Write("Investment Id: ");
                string id = Console.ReadLine();

                Console.Write("New Risk (1-5): ");
                int risk = int.Parse(Console.ReadLine());

                service.UpdateRisk(id, risk);
                break;

            case 3:
                Investment inv = new Investment();

                Console.Write("Id: ");
                inv.Id = Console.ReadLine();

                Console.Write("Asset Name: ");
                inv.AssetName = Console.ReadLine();

                Console.Write("Risk Rating: ");
                inv.RiskRating = int.Parse(Console.ReadLine());

                service.AddInvestment(inv);
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
