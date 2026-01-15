using System;
using System.Collections.Generic;

namespace Day20techstack
{
    public class Calculate
    {
        public static List<int> NumberList = new List<int>();
        public static void AddNumbers(int Numbers)
        {
            NumberList.Add(Numbers);
        }

        public static double GetGPAScore()
        {
            if (NumberList.Count == 0) return -1;
            double total = 0;
            foreach (var number in NumberList)
            {
                total += number;
            }
            return total / NumberList.Count;
        }

        public static char GetGradeScore(double gpa)
        {
            if (gpa < 5 || gpa > 10) return '\0';
            if (gpa == 10) return 'O';
            if (gpa >= 9) return 'A';
            if (gpa >= 8) return 'B';
            if (gpa >= 7) return 'C';
            if (gpa >= 6) return 'D';
            return 'E';
        }

        public static void Run()
        {
            Console.WriteLine("Enter number of Subjects: ");
            string? input = Console.ReadLine();
            int subjectCount;
            while (!int.TryParse(input, out subjectCount) || subjectCount <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive integer for number of Subjects:");
                input = Console.ReadLine();
            }
            for (int i = 0; i < subjectCount; i++)
            {
                Console.Write($"Enter marks for Subject {i + 1}: ");
                string? marksInput = Console.ReadLine();
                int marks;
                while (!int.TryParse(marksInput, out marks))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for marks:");
                    marksInput = Console.ReadLine();
                }
                AddNumbers(marks);
            }

            double gpa = GetGPAScore();
            if (gpa == -1)
            {
                Console.WriteLine("GPA cannot be calculated due to invalid marks.");
                return;
            }

            char grade = GetGradeScore(gpa);
            if (grade == '\0')
            {
                Console.WriteLine("Invalid GPA. Unable to determine grade.");
                return;
            }
            else
            {
                Console.WriteLine($"GPA: {gpa}");
                Console.WriteLine($"Grade: {grade}");
            }
        }
    }
}