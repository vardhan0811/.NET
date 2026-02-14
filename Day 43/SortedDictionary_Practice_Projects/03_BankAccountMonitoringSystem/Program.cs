using System;
using Domain;
using Services;
using Exceptions;

ManagementService service = new ManagementService();

while (true)
{
    Console.WriteLine("\n1 Display Accounts");
    Console.WriteLine("2 Deposit");
    Console.WriteLine("3 Withdraw");
    Console.WriteLine("4 Exit");

    Console.Write("Choice: ");
    int choice = int.Parse(Console.ReadLine());

    try
    {
        switch (choice)
        {
            case 1:
                foreach (var acc in service.GetAllAccounts())
                    Console.WriteLine($"{acc.Id} {acc.HolderName} {acc.Balance}");
                break;

            case 2:
                Console.Write("Account Id: ");
                string did = Console.ReadLine();

                Console.Write("Amount: ");
                decimal damt = decimal.Parse(Console.ReadLine());

                service.Deposit(did, damt);
                break;

            case 3:
                Console.Write("Account Id: ");
                string wid = Console.ReadLine();

                Console.Write("Amount: ");
                decimal wamt = decimal.Parse(Console.ReadLine());

                service.Withdraw(wid, wamt);
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
