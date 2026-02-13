using System;
using System.Collections.Generic;

namespace Day42
{
    public class CustomerSupport
    {
        public static void Run()
        {
            Console.WriteLine("===== CUSTOMER SUPPORT TICKET MERGE =====");
            Console.WriteLine("Instructions:");
            Console.WriteLine("1. Enter ticket IDs in ASCENDING order.");
            Console.WriteLine("2. Enter numbers separated by space.");
            Console.WriteLine("Example: 1 4 7");
            Console.WriteLine();

            Console.Write("Enter sorted tickets IDs for StreamA: ");
            List<int> streamA = ReadList();

            Console.WriteLine("Enter sorted tickets IDs for StreamB: ");
            List<int> streamB = ReadList();

            List<int> mergedTickets = new List<int>();

            int i = 0, j = 0;
            while(i<streamA.Count && j<streamB.Count)
            {
                if (streamA[i] < streamB[j])
                {
                    mergedTickets.Add(streamA[i]);
                    i++;
                }
                else
                {
                    mergedTickets.Add(streamB[j]);
                    j++;
                }
            }

            while(i<streamA.Count)
            {
                mergedTickets.Add(streamA[i]);
                i++;
            }

            while (j<streamB.Count)
            {
                mergedTickets.Add(streamB[j]);
                j++;
            }

            Console.WriteLine("\nMerged Ticket IDs:");
            Console.WriteLine(string.Join(" ", mergedTickets));
        }

        private static List<int> ReadList()
        {
            List<int> list = new List<int>();
            string input = Console.ReadLine();
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int number))
                {
                    list.Add(number);
                }
            }
            return list;
        }
    }
}