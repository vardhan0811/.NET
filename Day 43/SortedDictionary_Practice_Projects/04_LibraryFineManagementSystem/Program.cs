using System;
using Domain;
using Services;
using Exceptions;

ManagementService service = new ManagementService();

while (true)
{
    Console.WriteLine("\n1 Display Members by Fine");
    Console.WriteLine("2 Pay Fine");
    Console.WriteLine("3 Add Member");
    Console.WriteLine("4 Exit");

    Console.Write("Choice: ");
    int choice = int.Parse(Console.ReadLine());

    try
    {
        switch (choice)
        {
            case 1:
                foreach (var m in service.GetAllMembers())
                    Console.WriteLine($"{m.Id} {m.Name} Fine:{m.FineAmount}");
                break;

            case 2:
                Console.Write("Member Id: ");
                string id = Console.ReadLine();

                Console.Write("Amount Paid: ");
                int amt = int.Parse(Console.ReadLine());

                service.PayFine(id, amt);
                break;

            case 3:
                Member mem = new Member();

                Console.Write("Id: ");
                mem.Id = Console.ReadLine();

                Console.Write("Name: ");
                mem.Name = Console.ReadLine();

                Console.Write("Fine: ");
                mem.FineAmount = int.Parse(Console.ReadLine());

                service.AddMember(mem);
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
