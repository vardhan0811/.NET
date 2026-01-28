using System;
using System.Collections.Generic;

namespace Day20techstack
{
    /// <summary>
    /// Provides methods to calculate GPA and grade based on subject marks.
    /// </summary>
    public class Calculate
    {
        /// <summary>
        /// Stores the list of subject marks entered by the user.
        /// </summary>
        public static List<int> NumberList = new List<int>();

        /// <summary>
        /// Adds a subject mark to the NumberList.
        /// </summary>
        /// <param name="Numbers">Marks to add</param>
        public static void AddNumbers(int Numbers)
        {
            NumberList.Add(Numbers); // Add the mark to the list
        }

        /// <summary>
        /// Calculates the GPA score from the NumberList.
        /// </summary>
        /// <returns>GPA as double, or -1 if no marks present</returns>
        public static double GetGPAScore()
        {
            if (NumberList.Count == 0) return -1; // No marks entered
            double total = 0;
            foreach (var number in NumberList)
            {
                total += number; // Sum all marks
            }
            return total / NumberList.Count; // Calculate average
        }

        /// <summary>
        /// Determines the grade based on the GPA score.
        /// </summary>
        /// <param name="gpa">GPA score</param>
        /// <returns>Grade as a character</returns>
        public static char GetGradeScore(double gpa)
        {
            if (gpa < 5 || gpa > 10) return '\0'; // Invalid GPA
            if (gpa == 10) return 'O';
            if (gpa >= 9) return 'A';
            if (gpa >= 8) return 'B';
            if (gpa >= 7) return 'C';
            if (gpa >= 6) return 'D';
            return 'E'; // For GPA between 5 and 6
        }

        /// <summary>
        /// Runs the GPA and grade calculation process by taking user input.
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("Enter number of Subjects: ");
            string? input = Console.ReadLine();
            int subjectCount;
            // Validate subject count input
            while (!int.TryParse(input, out subjectCount) || subjectCount <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive integer for number of Subjects:");
                input = Console.ReadLine();
            }
            // Loop to get marks for each subject
            for (int i = 0; i < subjectCount; i++)
            {
                Console.Write($"Enter marks for Subject {i + 1}: ");
                string? marksInput = Console.ReadLine();
                int marks;
                // Validate marks input
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