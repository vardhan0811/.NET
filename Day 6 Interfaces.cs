using System;
using System.Collections.Generic;


public class Day6Interfaces
{
    #region Interfaces
    public interface IGreeter
    {
        string Name { get; }
        void Greet();
    }

    public class Employee : IGreeter
    {
        public string Name { get; private set; }
        public string Position { get; private set; }

        public Employee(string name, string position)
        {
            Name = name;
            Position = position;
        }

        public void Greet()
        {
            Console.WriteLine($"Hello, I am {Name}, working as {Position}.");
        }
    }

    public class HRManager
    {
        private List<Employee> employees = new List<Employee>();
        public void AddEmployee(string name, string position)
        {
            employees.Add(new Employee(name, position));
        }

        public void RemoveEmployee(string name)
        {
            employees.RemoveAll(e => e.Name == name);
        }

        public void ListEmployees()
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.Name} - {emp.Position}");
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return employees;
        }
    }
    #endregion

    // Department class representing a department in the organization
    public class Department
    {
        public int DeptId { get; }
        public string? DeptName { get; }

        public Department(int deptId, string deptName)
        {
            DeptId = deptId;
            DeptName = deptName;
        }
    }

    // Subject class representing a subject in the organization
    public class Subject
    {
        public string SubjectCode { get; }
        public string SubjectName { get; }
        public int Credits { get; }

        public Subject(string subjectCode, string subjectName, int credits)
        {
            SubjectCode = subjectCode;
            SubjectName = subjectName;
            Credits = credits;
        }
    }

    // Semester class representing a semester in the organization
    public class Semester
    {
        public string SemesterName { get; }
        public int Year { get; }

        public Semester(string semesterName, int year)
        {
            SemesterName = semesterName;
            Year = year;
        }
    }

    // Examiner class representing an examiner in the organization
    public class Examiner
    {
        public int EmployeeId { get; }
        public string Name { get; }
        public string Specialization { get; }

        public Examiner(int employeeId, string name, string specialization)
        {
            EmployeeId = employeeId;
            Name = name;
            Specialization = specialization;
        }
    }

    // ExamSchedule class representing the schedule of an exam
    public class ExamSchedule
    {
        public DateTime ExamDate { get; }
        public string Time { get; }
        public string Venue { get; }

        public ExamSchedule(DateTime examDate, string time, string venue)
        {
            ExamDate = examDate;
            Time = time;
            Venue = venue;
        }
    }

    // Exam class representing an exam in the organization
    public class Exam
    {
        public int ExamId { get; }
        public Subject Subject { get; }
        public Semester Semester { get; }
        public Examiner? Examiner { get; private set; }
        public ExamSchedule? Schedule { get; private set; }

        public Exam(int examId, Subject subject, Semester semester)
        {
            ExamId = examId;
            Subject = subject;
            Semester = semester;
        }

        public void AssignExaminer(Examiner examiner)
        {
            Examiner = examiner;
        }

        public void SetSchedule(ExamSchedule schedule)
        {
            Schedule = schedule;
        }

        public void ShowExamDetails()
        {
            Console.WriteLine("----------------------------------");

            Console.WriteLine($"Exam ID: {ExamId}");
            Console.WriteLine($"Subject: {Subject.SubjectName}");
            Console.WriteLine($"Semester: {Semester.SemesterName} {Semester.Year}");
            Console.WriteLine($"Examiner: {Examiner?.Name ?? "Not Assigned"}");

            if (Schedule != null)
            {
                Console.WriteLine($"Date: {Schedule.ExamDate.ToShortDateString()}");
                Console.WriteLine($"Time: {Schedule.Time}");
                Console.WriteLine($"Venue: {Schedule.Venue}");
            }

            Console.WriteLine("----------------------------------");
        }
    }


    public class HOD
    {
        public int HodId { get; }
        public string Name { get; }
        public Department Department { get; }

        private List<Exam> exams = new List<Exam>();

        public HOD(int hodId, string name, Department department)
        {
            HodId = hodId;
            Name = name;
            Department = department;
        }

        public void ScheduleExam(Exam exam)
        {
            exams.Add(exam);
        }

        public void AssignExaminer(Exam exam, Examiner examiner)
        {
            exam.AssignExaminer(examiner);
        }

        public void ListExams()
        {
            foreach (var exam in exams)
            {
                exam.ShowExamDetails();
            }
        }
    }

    interface IPlatter
    {
        void Serve();
        string GetDescription();
    }
    public class VegPlatter : IPlatter
    {
        public void Serve()
        {
            Console.WriteLine("Serving a vegetarian platter.");
        }

        public string GetDescription()
        {
            return "A delicious vegetarian platter with assorted veggies.";
        }
    }

    public class NonVegPlatter : IPlatter
    {
        public void Serve()
        {
            Console.WriteLine("Serving a non-vegetarian platter.");
        }

        public string GetDescription()
        {
            return "A delicious non-vegetarian platter with assorted meats.";
        }
    }

    public static void Run()
    {
        Department cse = new Department(1, "Computer Science");
        HOD hod = new HOD(101, "Dr. Rao", cse);

        Subject math = new Subject("M101", "Mathematics", 4);
        Semester sem1 = new Semester("Semester 1", 2025);

        Exam exam1 = new Exam(1, math, sem1);

        Examiner examiner = new Examiner(5001, "Dr. Smith", "Mathematics");
        ExamSchedule schedule = new ExamSchedule(
            new DateTime(2025, 12, 24),
            "10:00 AM - 1:00 PM",
            "Hall A"
        );

        hod.ScheduleExam(exam1);
        hod.AssignExaminer(exam1, examiner);
        exam1.SetSchedule(schedule);
        hod.ListExams();


        IPlatter vegPlatter = new VegPlatter();
        vegPlatter.Serve();
        Console.WriteLine(vegPlatter.GetDescription());

        IPlatter nonVegPlatter = new NonVegPlatter();
        nonVegPlatter.Serve();
        Console.WriteLine(nonVegPlatter.GetDescription());
    }

    
}