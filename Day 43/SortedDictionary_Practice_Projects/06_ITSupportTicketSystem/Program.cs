using System;
using Domain;
using Services;
using Exceptions;

ManagementService service = new ManagementService();

while (true)
{
    Console.WriteLine("\n1 Display Tickets by Priority");
    Console.WriteLine("2 Process Next Ticket");
    Console.WriteLine("3 Escalate Ticket");
    Console.WriteLine("4 Add Ticket");
    Console.WriteLine("5 Exit");

    Console.Write("Choice: ");
    int choice = int.Parse(Console.ReadLine());

    try
    {
        switch (choice)
        {
            case 1:
                foreach (var t in service.GetAll())
                    Console.WriteLine($"{t.Id} {t.IssueDescription} Severity:{t.SeverityLevel}");
                break;

            case 2:
                var processed = service.ProcessNext();
                Console.WriteLine($"Processed: {processed.Id}");
                break;

            case 3:
                Console.Write("Ticket Id: ");
                string id = Console.ReadLine();
                service.Escalate(id);
                break;

            case 4:
                SupportTicket tk = new SupportTicket();

                Console.Write("Id: ");
                tk.Id = Console.ReadLine();

                Console.Write("Issue: ");
                tk.IssueDescription = Console.ReadLine();

                Console.Write("Severity (1-5): ");
                tk.SeverityLevel = int.Parse(Console.ReadLine());

                service.AddTicket(tk);
                break;

            case 5:
                return;
        }
    }
    catch (CustomBaseException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
