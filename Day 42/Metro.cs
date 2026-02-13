using System;
using System.Collections.Generic;

namespace Day42
{
    public class Metro
    {
        public static void Run()
        {
            Queue<(TimeSpan entryTime, string ticketType)> metroQueue = new Queue<(TimeSpan, string)>();
            Console.WriteLine("Enter number of passengers: ");
            int passengerCount = int.Parse(Console.ReadLine() ?? "0");

            for(int i=1;i<=passengerCount;i++)
            {
                Console.WriteLine($"\nPassenger {i}: ");
                Console.Write("Enter Entry Time (HH:mm): ");
                TimeSpan time = TimeSpan.Parse(Console.ReadLine() ?? "00:00");

                Console.Write("Enter Ticket Type: ");
                string? ticketType = Console.ReadLine();

                metroQueue.Enqueue((time, ticketType ?? "Unknown"));
            }

            TimeSpan peakStart = new TimeSpan(8, 0, 0);
            TimeSpan peakEnd = new TimeSpan(10, 0, 0);

            int count = 0;

            while(metroQueue.Count>0)
            {
                var passenger = metroQueue.Dequeue();
                TimeSpan time = passenger.entryTime;
                string type = passenger.ticketType;

                if ((type != "Regular" && type != "Premium"))
                    continue;

                if(type=="Regular" && time >= peakStart && time <= peakEnd) {
                {
                    count++;
                }
            }

            Console.WriteLine($"\nNumber of passengers during peak hours: {count}");
        }
    }
}