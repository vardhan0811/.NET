using System;
using System.Collections.Generic;

// Delegate Declaration
delegate int StudentComparer(Students s1, Students s2);

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

    // Display student details
    public void Display()
    {
        Console.WriteLine($"ID: {StudentId}, Name: {StudentName}");
        foreach (var m in SubjectMarks)
        {
            Console.WriteLine($"{m.Key}: {m.Value}");
        }
        Console.WriteLine($"Average: {GetAverageMarks():F2}");
        Console.WriteLine();
    }
}

class Delegates
{
    // Method matching delegate signature
    static int CompareStudentsByAverage(Students s1, Students s2)
    {
        // Sort descending by average marks
        return s2.GetAverageMarks().CompareTo(s1.GetAverageMarks());
    }

    // Entry point for this class
    public static void Run()
    {
        List<Students> students = new List<Students>();

        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());

        // Input student details
        for (int i = 1; i <= n; i++)
        {
            Console.WriteLine($"\nStudent {i}");

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Maths: ");
            int m = int.Parse(Console.ReadLine());

            Console.Write("Science: ");
            int s = int.Parse(Console.ReadLine());

            Console.Write("English: ");
            int e = int.Parse(Console.ReadLine());

            students.Add(new Students(id, name, m, s, e));
        }

        // Display all students
        Console.WriteLine("\n--- All Students ---");
        foreach (var st in students)
        {
            st.Display();
        }

        // Create delegate instance
        StudentComparer comparer = CompareStudentsByAverage;

        // Sort using delegate
        students.Sort(new Comparison<Students>(comparer));

        Console.WriteLine("\n--- Sorted by Average (High to Low) ---");
        foreach (var st in students)
        {
            Console.WriteLine($"{st.StudentName} - {st.GetAverageMarks():F2}");
        }

        // Display topper
        Students topper = students[0];
        Console.WriteLine("\n--- Topper ---");
        Console.WriteLine($"{topper.StudentName} with Average {topper.GetAverageMarks():F2}");
    }
}

// To run this code, add the following line in your Main method:
// Delegates.Run();
