using System;
using System.IO;

namespace Day19FileHandling{
    public class Day19FileHandling
    {
        public static void Run(string[] args)
        {
            Console.WriteLine("Day 19 File Handling - Placeholder");
            string[] lines = {"First Line", "Second Line"};
            using (StreamWriter writer = new StreamWriter("output.txt"))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}