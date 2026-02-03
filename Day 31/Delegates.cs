using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentDelegateExample
{
    // Student Class
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public Dictionary<string, int> SubjectMarks { get; set; }

        public Student(int id, string name, int maths, int science, int english)
        {
            StudentId = id;
            StudentName = name ?? throw new ArgumentNullException(nameof(name));

            SubjectMarks = new Dictionary<string, int>
            {
                { "Maths", maths },
                { "Science", science },
                { "English", english }
            };
        }

        // Calculate Average Marks
        public double GetAverageMarks()
        {
            return SubjectMarks.Values.Average();
        }

        // Display Details
        public void Display()
        {
            Console.WriteLine($"ID   : {StudentId}");
            Console.WriteLine($"Name : {StudentName}");

            foreach (var mark in SubjectMarks)
            {
                Console.WriteLine($"{mark.Key,-8}: {mark.Value}");
            }

            Console.WriteLine($"Average : {GetAverageMarks():F2}");
            Console.WriteLine(new string('-', 30));
        }
    }

    public class Program
    {
        static void Main()
        {
            Run();
        }

        public static void Run()
        {
            List<Student> students = new();

            int count = ReadInt("Enter number of students: ");

            // Read Student Data
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"\nStudent {i}");

                int id = ReadInt("ID: ");
                string name = ReadString("Name: ");

                int maths = ReadInt("Maths: ");
                int science = ReadInt("Science: ");
                int english = ReadInt("English: ");

                students.Add(new Student(id, name, maths, science, english));
            }

            // Display All Students
            Console.WriteLine("\n--- All Students ---");

            foreach (var student in students)
            {
                student.Display();
            }

            // Sort using Comparison Delegate (Lambda)
            students.Sort((s1, s2) =>
                s2.GetAverageMarks().CompareTo(s1.GetAverageMarks()));

            // Display Sorted List
            Console.WriteLine("\n--- Sorted by Average (High to Low) ---");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentName} - {student.GetAverageMarks():F2}");
            }

            // Display Topper
            if (students.Count > 0)
            {
                Student topper = students[0];

                Console.WriteLine("\n--- Topper ---");
                Console.WriteLine($"{topper.StudentName} : {topper.GetAverageMarks():F2}");
            }
        }

        // Helper: Read Integer Safely
        private static int ReadInt(string message)
        {
            int value;

            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.WriteLine("❌ Invalid input. Please enter a number.");
            }
        }

        // Helper: Read String Safely
        private static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);

                string input = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("❌ Input cannot be empty.");
            }
        }
    }
}
