using System;
using System.Collections.Generic;
using System.Linq;

namespace Day42
{
    public class LeardBoard
    {
        public static void Run()
        {
            List<(string name, int score)> players = new List<(string, int)>();
            Console.Write("Enter number of players: ");
            int playerCount = int.Parse(Console.ReadLine() ?? "0");

            for(int i=1;i<=playerCount;i++)
            {
                Console.WriteLine($"\nPlayer {i}: ");
                Console.Write("Enter Name: ");
                string? name = Console.ReadLine();

                Console.Write("Enter Score: ");
                int score = int.Parse(Console.ReadLine());

                players.Add(name,  score);
            }

            Console.Write("\nEnter value of K: ");
            int k = int.Parse(Console.ReadLine());

            vat topK = players
                .OrderByDescending(p => p.score)
                .ThenBy(p => p.name)
                .Take(k)
                .ToList();

            Console.WriteLine("\n--- Top K Players ---");
            foreach(var player in topK)
            {
                Console.WriteLine($"Name: {player.name}, Score: {player.score}");
            }
        }
    }
}