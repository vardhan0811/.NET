using System;
using System.Collections;

namespace Day20techstack
{
    public class Yoga
    {
        public class MeditationCenter
        {
            public int MemberID { get; set; }
            public int Age { get; set; }
            public double Weight { get; set; }
            public double Height { get; set; }  
            public string? Goal { get; set; }
            public double BMI { get; set; }
        }

        public static ArrayList MemberList = new ArrayList();
        public static void AddYogaMember(int memberId, int age, double weight, double height, string goal)
        {
            MeditationCenter member = new MeditationCenter
            {
                MemberID = memberId,
                Age = age,
                Weight = weight,
                Height = height,
                Goal = goal,
                BMI = weight / (height * height)
            };
            MemberList.Add(member);
        }

        public static double CalculateBMI(int memberId)
        {
            foreach (MeditationCenter member in MemberList)
            {
                if (member.MemberID == memberId)
                {
                    double bmi = member.Weight / (member.Height * member.Height);
                    bmi = Math.Floor(bmi * 100)/100;
                    member.BMI = bmi;
                    return bmi;
                }
            }
            return -1; // Member not found
        }

        public static int CalculateYogaFee(int memberId)
        {
            foreach (MeditationCenter member in MemberList)
            {
                if (member.MemberID == memberId)
                {
                    if (!string.IsNullOrEmpty(member.Goal) && member.Goal.Equals("Weight Loss", StringComparison.OrdinalIgnoreCase))
                    {
                        if (member.BMI >= 25 && member.BMI < 30) return 2000;
                        if (member.BMI >= 30 && member.BMI < 35) return 2500;
                        if (member.BMI >= 35) return 3000;
                        return 0;
                    }
                    else if (!string.IsNullOrEmpty(member.Goal) && member.Goal.Equals("Weight Gain", StringComparison.OrdinalIgnoreCase))
                    {
                        return 2500;
                    }
                }
            }
            return -1; // Member not found
        }

        public static void Run()
        {
            Console.WriteLine("Enter number of Yoga Members to add:");
            int count = int.Parse(Console.ReadLine() ?? "0");

            for(int i = 0; i < count; i++)
            {
                Console.WriteLine($"Enter details for Yoga Member {i + 1}:");
                Console.Write("Member ID: ");
                int memberId = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Age: ");
                int age = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Weight (kg): ");
                double weight = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Height (m): ");
                double height = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Goal (Weight Loss/Weight Gain): ");
                string goal = Console.ReadLine() ?? "";

                AddYogaMember(memberId, age, weight, height, goal);
            }

            Console.WriteLine("Enter Member ID to calculate BMI and Yoga Fee:");
            int inputMemberId = int.Parse(Console.ReadLine() ?? "0");

            double bmi = CalculateBMI(inputMemberId);
            if (bmi != -1)
            {
                Console.WriteLine($"BMI for Member ID {inputMemberId}: {bmi}");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }

            int fee = CalculateYogaFee(inputMemberId);
            if (fee == -1)
            {
                Console.WriteLine("Member not found.");
            }
            else if (fee == 0)
            {
                Console.WriteLine("No fee applicable based on BMI and Goal.");
            }
            else
            {
                Console.WriteLine($"Yoga Fee for Member ID {inputMemberId}: {fee}");
            }
        }
    }
}