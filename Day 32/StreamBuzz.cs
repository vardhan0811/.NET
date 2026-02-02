using System;
using System.Collections.Generic;
using System.Linq;

namespace Day32
{
    // Class to store creator statistics
    public class CreatorStats
    {
        public string CreatorName { get; set; } // Name of the creator
        public double[] WeeklyLikes { get; set; } // Array to store weekly likes
        public static List<CreatorStats> EngagementBoard { get; } = new List<CreatorStats>(); // Static list to hold all creator records
    }

    public class StreamBuzz
    {
        // Registers a new creator by adding their record to the EngagementBoard
        public void RegisterCreator(CreatorStats record)
        {
            CreatorStats.EngagementBoard.Add(record);
        }

        // Returns a dictionary of creators and their count of posts above the like threshold
        public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
        {
            var result = new Dictionary<string, int>();
            foreach (var creator in records)
            {
                // Count the number of weeks where likes are above or equal to the threshold
                int count = creator.WeeklyLikes.Count(like => like >= likeThreshold);
                if (count > 0)
                {
                    result[creator.CreatorName] = count;
                }
            }
            return result;
        }

        // Calculates the average likes across all creators and weeks
        public double CalculateAverageLikes()
        {
            var allLikes = CreatorStats.EngagementBoard.SelectMany(c => c.WeeklyLikes).ToList();
            if (allLikes.Count == 0) return 0;
            return Math.Round(allLikes.Average());
        }

        // Main method: Entry point for the console application
        public static void Main()
        {
            StreamBuzz p = new StreamBuzz();
            while (true)
            {
                // Display menu options
                Console.WriteLine("1. Register Creator");
                Console.WriteLine("2. Show Top Posts");
                Console.WriteLine("3. Calculate Average Likes");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    // Register a new creator
                    Console.WriteLine("Enter Creator Name:");
                    string name = Console.ReadLine();
                    double[] likes = new double[4];
                    Console.WriteLine("Enter weekly likes (Week 1 to 4):");
                    for (int i = 0; i < 4; i++)
                    {
                        likes[i] = double.Parse(Console.ReadLine());
                    }
                    CreatorStats record = new CreatorStats { CreatorName = name, WeeklyLikes = likes };
                    p.RegisterCreator(record);
                    Console.WriteLine("Creator registered successfully\n");
                }
                else if (choice == "2")
                {
                    // Show creators with posts above the like threshold
                    Console.WriteLine("Enter like threshold:");
                    double threshold = double.Parse(Console.ReadLine());
                    var result = p.GetTopPostCounts(CreatorStats.EngagementBoard, threshold);
                    if (result.Count == 0)
                    {
                        Console.WriteLine("No top-performing posts this week\n");
                    }
                    else
                    {
                        foreach (var kvp in result)
                        {
                            Console.WriteLine($"{kvp.Key} - {kvp.Value}");
                        }
                        Console.WriteLine();
                    }
                }
                else if (choice == "3")
                {
                    // Calculate and display the average likes
                    double avg = p.CalculateAverageLikes();
                    Console.WriteLine($"Overall average weekly likes: {avg}\n");
                }
                else if (choice == "4")
                {
                    // Exit the application
                    Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                    break;
                }
            }
        }
    }
}