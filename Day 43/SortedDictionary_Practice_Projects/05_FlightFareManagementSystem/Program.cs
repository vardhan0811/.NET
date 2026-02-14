using System;
using Domain;
using Services;
using Exceptions;

ManagementService service = new ManagementService();

while (true)
{
    Console.WriteLine("\n1 Display Tickets");
    Console.WriteLine("2 Update Fare");
    Console.WriteLine("3 Add Ticket");
    Console.WriteLine("4 Exit");

    Console.Write("Choice: ");
    int choice = int.Parse(Console.ReadLine());

    try
    {
        switch (choice)
        {
            case 1:
                foreach (var t in service.GetAllTickets())
                    Console.WriteLine($"{t.Id} {t.PassengerName} Fare:{t.Fare}");
                break;

            case 2:
                Console.Write("Ticket Id: ");
                string id = Console.ReadLine();

                Console.Write("New Fare: ");
                int fare = int.Parse(Console.ReadLine());

                service.UpdateFare(id, fare);
                break;

            case 3:
                Ticket tk = new Ticket();

                Console.Write("Id: ");
                tk.Id = Console.ReadLine();

                Console.Write("Passenger Name: ");
                tk.PassengerName = Console.ReadLine();

                Console.Write("Fare: ");
                tk.Fare = int.Parse(Console.ReadLine());

                service.AddTicket(tk);
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
