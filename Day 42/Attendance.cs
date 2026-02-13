using System;
using System.Collections.Generic;

namespace Day42
{
    public class Attendance
    {
        public static void Run()
        {
            HashSet<int> seenIds = new HashSet<int>();
            List<int> firstEntries = new List<int>();

            Console.WriteLine("Enter number of attendance entries: ");
            int entryCount = int.Parse(Console.ReadLine( ?? "0"));

            for (int i = 0; i <= entryCount; i++)
            {
                Console.WriteLine($"Enter employee ID for entry {i}: ");
                int empId = int.Parse(Console.ReadLine() ?? "0");

                if (!seenIds.Contains(empId))
                {
                    seenIds.Add(empId);
                    firstEntries.Add(empId);
                }
            }

            Console.WriteLine("\nUnique Employee IDs in order of first entry:");
            foreach (int id in firstEntries)
            {
                Console.Write(id + " ");
            }
            Console.WriteLine();
        }
    }
}