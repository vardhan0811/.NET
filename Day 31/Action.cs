using System;
using System.Collections.Generic;
using System.Linq;

namespace Action
{
// Student class
class Students
{
    public int StudentId;
    public string StudentName;
    public Dictionary<string, int> SubjectMarks;

    public Students(int id, string name, int m, int s, int e)
    {
        StudentId = id;
        StudentName = name;

        SubjectMarks = new Dictionary<string, int>()
        {
            { "Maths", m },
            { "Science", s },
            { "English", e }
        };
    }

    // Calculate Average
    public double GetAverageMarks()
    {
        int total = 0;

        foreach (var m in SubjectMarks)
        {
            total += m.Value;
        }

        return (double)total / SubjectMarks.Count;
    }
}

class Action
{
    public static void Run()
    {
        List<Students> students = new List<Students>();

        Console.Write("Enter number of students: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }
        int n = int.Parse(input);

        // Input students
        for (int i = 1; i <= n; i++)
        {
            Console.WriteLine($"\nStudent {i}");

            Console.Write("ID: ");
            string? idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                return;
            }
            int id = int.Parse(idInput);

            Console.Write("Name: ");
            string? nameInput = Console.ReadLine();
            string name = nameInput ?? string.Empty;

            Console.Write("Maths: ");
            string? mathsInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(mathsInput) || !int.TryParse(mathsInput, out int m))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for Maths.");
                return;
            }

            Console.Write("Science: ");
            string? scienceInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(scienceInput) || !int.TryParse(scienceInput, out int s))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for Science.");
                return;
            }

            Console.Write("English: ");
            string? englishInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(englishInput) || !int.TryParse(englishInput, out int e))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for English.");
                return;
            }

            students.Add(new Students(id, name, m, s, e));
        }

        // -------------------------------
        // FUNC → Calculate Average
        // -------------------------------
        Func<Students, double> getAverage =
            student => student.GetAverageMarks();


        // -------------------------------
        // ACTION → Display Student
        // -------------------------------
        Action<Students> displayStudent = student =>
        {
            Console.WriteLine($"ID: {student.StudentId}, Name: {student.StudentName}");

            foreach (var m in student.SubjectMarks)
            {
                Console.WriteLine($"{m.Key}: {m.Value}");
            }

            Console.WriteLine($"Average: {getAverage(student):F2}");
            Console.WriteLine();
        };


        // -------------------------------
        // PREDICATE → Check Pass (>=40)
        // -------------------------------
        Predicate<Students> isPassed =
            student => getAverage(student) >= 40;


        // -------------------------------
        // Display All Students
        // -------------------------------
        Console.WriteLine("\n--- All Students ---");

        foreach (var s in students)
        {
            displayStudent(s);
        }


        // -------------------------------
        // SORT using FUNC
        // -------------------------------
        var sorted = students
                        .OrderByDescending(s => getAverage(s))
                        .ToList();

        Console.WriteLine("\n--- Sorted by Average (High to Low) ---");

        foreach (var s in sorted)
        {
            Console.WriteLine($"{s.StudentName} - {getAverage(s):F2}");
        }


        // -------------------------------
        // TOPPER
        // -------------------------------
        Students topper = sorted[0];

        Console.WriteLine("\n--- Topper ---");
        Console.WriteLine($"{topper.StudentName} with Average {getAverage(topper):F2}");


        // -------------------------------
        // PASSED STUDENTS (Predicate)
        // -------------------------------
        Console.WriteLine("\n--- Passed Students (Average >= 40) ---");

        var passed = students.FindAll(isPassed);

        foreach (var s in passed)
        {
            Console.WriteLine(s.StudentName);
        }
    }
}
}