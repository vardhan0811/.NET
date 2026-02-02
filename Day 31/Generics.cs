using System;
using System.Collections.Generic;
using System.Linq;

// Student class to store student details and marks
class Student
{
    public int StudentId;
    public string StudentName;
    public Dictionary<string, int> SubjectMarks;

    // Constructor to initialize student details and marks
    public Student(int studentId, string studentName, int mathsMarks, int scienceMarks, int englishMarks)
    {
        StudentId = studentId;
        StudentName = studentName;

        // Store subject marks in a dictionary
        SubjectMarks = new Dictionary<string, int>()
        {
            { "Maths", mathsMarks },
            { "Science", scienceMarks },
            { "English", englishMarks }
        };
    }

    // Calculate Average Marks
    public double GetAverageMarks()
    {
        int totalMarks = 0;

        // Sum all subject marks
        foreach (var subjectMark in SubjectMarks)
        {
            totalMarks += subjectMark.Value;
        }

        // Return average
        return (double)totalMarks / SubjectMarks.Count;
    }

    // Display student details and marks
    public void DisplayStudentDetails()
    {
        Console.WriteLine($"ID: {StudentId}, Name: {StudentName}");

        // Display each subject and its marks
        foreach (var subjectMark in SubjectMarks)
        {
            Console.WriteLine($"{subjectMark.Key}: {subjectMark.Value}");
        }

        // Display average marks
        Console.WriteLine($"Average: {GetAverageMarks():F2}");
        Console.WriteLine();
    }
}

class Generics
{
    static void Run()
    {
        List<Student> studentList = new List<Student>();

        // Ask user for number of students
        Console.Write("Enter number of students: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }
        int numberOfStudents = int.Parse(input);

        // Taking input for each student
        for (int studentIndex = 1; studentIndex <= numberOfStudents; studentIndex++)
        {
            Console.WriteLine($"\nEnter details for student {studentIndex}");

            // Input student ID
            Console.Write("ID: ");
            string? idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                return;
            }
            int studentId = int.Parse(idInput);

            // Input student name
            Console.Write("Name: ");
            string? nameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.WriteLine("Invalid input. Please enter a valid name.");
                return;
            }
            string studentName = nameInput;

            // Input Maths marks
            Console.Write("Maths: ");
            string? mathsInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(mathsInput) || !int.TryParse(mathsInput, out int mathsMarks))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for Maths.");
                return;
            }

            // Input Science marks
            Console.Write("Science: ");
            string? scienceInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(scienceInput) || !int.TryParse(scienceInput, out int scienceMarks))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for Science.");
                return;
            }

            // Input English marks
            Console.Write("English: ");
            string? englishInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(englishInput) || !int.TryParse(englishInput, out int englishMarks))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for English.");
                return;
            }

            // Add student to the list
            studentList.Add(new Student(studentId, studentName, mathsMarks, scienceMarks, englishMarks));
        }

        // Display All Students
        Console.WriteLine("\n--- All Students ---");
        foreach (var student in studentList)
        {
            student.DisplayStudentDetails();
        }

        // Sort students by average marks in descending order
        var sortedStudents = studentList.OrderByDescending(student => student.GetAverageMarks()).ToList();

        Console.WriteLine("\n--- Sorted by Average (High to Low) ---");

        // Display sorted students with their average marks
        foreach (var student in sortedStudents)
        {
            Console.WriteLine($"{student.StudentName} - {student.GetAverageMarks():F2}");
        }

        // Find Topper (student with highest average)
        Student topperStudent = sortedStudents[0];

        Console.WriteLine("\n--- Topper ---");
        Console.WriteLine($"{topperStudent.StudentName} with Average {topperStudent.GetAverageMarks():F2}");
    }
}
