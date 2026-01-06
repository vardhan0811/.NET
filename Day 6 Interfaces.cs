using System;
using System;
using System.Collections.Generic;

/// <summary>
/// Demonstrates interfaces, implementation, and organization structure in C#.
/// </summary>
public class Day6Interfaces
{
    #region Interfaces
    /// <summary>
    /// Interface for greeting functionality.
    /// </summary>
    public interface IGreeter
    {
        /// <summary>
        /// Gets the name of the greeter.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Greets with a message.
        /// </summary>
        void Greet();
    }

    /// <summary>
    /// Represents an employee implementing IGreeter.
    /// </summary>
    public class Employee : IGreeter
    {
        /// <summary>
        /// Gets the employee's name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the employee's position.
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        /// Initializes a new employee.
        /// </summary>
        /// <param name="name">Employee name.</param>
        /// <param name="position">Employee position.</param>
        public Employee(string name, string position)
        {
            Name = name;
            Position = position;
        }

        /// <summary>
        /// Greets with employee details.
        /// </summary>
        public void Greet()
        {
            Console.WriteLine($"Hello, I am {Name}, working as {Position}.");
        }
    }

    /// <summary>
    /// Manages employees in an organization.
    /// </summary>
    public class HRManager
    {
        private List<Employee> employees = new List<Employee>();
        /// <summary>
        /// Adds an employee.
        /// </summary>
        /// <param name="name">Employee name.</param>
        /// <param name="position">Employee position.</param>
        public void AddEmployee(string name, string position)
        {
            employees.Add(new Employee(name, position));
        }

        /// <summary>
        /// Removes an employee by name.
        /// </summary>
        /// <param name="name">Employee name.</param>
        public void RemoveEmployee(string name)
        {
            employees.RemoveAll(e => e.Name == name);
        }

        /// <summary>
        /// Lists all employees.
        /// </summary>
        public void ListEmployees()
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.Name} - {emp.Position}");
            }
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>Enumerable of employees.</returns>
        public IEnumerable<Employee> GetEmployees()
        {
            return employees;
        }
    }
    #endregion

    /// <summary>
    /// Represents a department in the organization.
    /// </summary>
    public class Department
    {
            /// <summary>
            /// Gets the department ID.
            /// </summary>
            public int DeptId { get; }
            /// <summary>
            /// Gets the department name.
            /// </summary>
            public string? DeptName { get; }

            /// <summary>
            /// Initializes a new department.
            /// </summary>
            /// <param name="deptId">Department ID.</param>
            /// <param name="deptName">Department name.</param>
            public Department(int deptId, string deptName)
            {
                DeptId = deptId;
                DeptName = deptName;
            }
    }

        /// <summary>
        /// Represents a subject in the organization.
        /// </summary>
        public class Subject
        {
            /// <summary>
            /// Gets the subject code.
            /// </summary>
            public string SubjectCode { get; }
            /// <summary>
            /// Gets the subject name.
            /// </summary>
            public string SubjectName { get; }
            /// <summary>
            /// Gets the number of credits.
            /// </summary>
            public int Credits { get; }

            /// <summary>
            /// Initializes a new subject.
            /// </summary>
            /// <param name="subjectCode">Subject code.</param>
            /// <param name="subjectName">Subject name.</param>
            /// <param name="credits">Number of credits.</param>
            public Subject(string subjectCode, string subjectName, int credits)
            {
                SubjectCode = subjectCode;
                SubjectName = subjectName;
                Credits = credits;
            }
        }

        /// <summary>
        /// Represents a semester in the organization.
        /// </summary>
        public class Semester
        {
            /// <summary>
            /// Gets the semester name.
            /// </summary>
            public string SemesterName { get; }
            /// <summary>
            /// Gets the year of the semester.
            /// </summary>
            public int Year { get; }

            /// <summary>
            /// Initializes a new semester.
            /// </summary>
            /// <param name="semesterName">Semester name.</param>
            /// <param name="year">Year.</param>
            public Semester(string semesterName, int year)
            {
                SemesterName = semesterName;
                Year = year;
            }
        }

        /// <summary>
        /// Represents an examiner in the organization.
        /// </summary>
        public class Examiner
        {
            /// <summary>
            /// Gets the employee ID.
            /// </summary>
            public int EmployeeId { get; }
            /// <summary>
            /// Gets the examiner's name.
            /// </summary>
            public string Name { get; }
            /// <summary>
            /// Gets the examiner's specialization.
            /// </summary>
            public string Specialization { get; }

            /// <summary>
            /// Initializes a new examiner.
            /// </summary>
            /// <param name="employeeId">Employee ID.</param>
            /// <param name="name">Examiner name.</param>
            /// <param name="specialization">Specialization.</param>
            public Examiner(int employeeId, string name, string specialization)
            {
                EmployeeId = employeeId;
                Name = name;
                Specialization = specialization;
            }
        }

        /// <summary>
        /// Represents the schedule of an exam.
        /// </summary>
        public class ExamSchedule
        {
            /// <summary>
            /// Gets the exam date.
            /// </summary>
            public DateTime ExamDate { get; }
            /// <summary>
            /// Gets the exam time.
            /// </summary>
            public string Time { get; }
            /// <summary>
            /// Gets the exam venue.
            /// </summary>
            public string Venue { get; }

            /// <summary>
            /// Initializes a new exam schedule.
            /// </summary>
            /// <param name="examDate">Exam date.</param>
            /// <param name="time">Exam time.</param>
            /// <param name="venue">Exam venue.</param>
            public ExamSchedule(DateTime examDate, string time, string venue)
            {
                ExamDate = examDate;
                Time = time;
                Venue = venue;
            }
        }

        /// <summary>
        /// Represents an exam in the organization.
        /// </summary>
        public class Exam
        {
            /// <summary>
            /// Gets the exam ID.
            /// </summary>
            public int ExamId { get; }
            /// <summary>
            /// Gets the subject of the exam.
            /// </summary>
            public Subject Subject { get; }
            /// <summary>
            /// Gets the semester of the exam.
            /// </summary>
            public Semester Semester { get; }
            /// <summary>
            /// Gets the assigned examiner.
            /// </summary>
            public Examiner? Examiner { get; private set; }
            /// <summary>
            /// Gets the exam schedule.
            /// </summary>
            public ExamSchedule? Schedule { get; private set; }

            /// <summary>
            /// Initializes a new exam.
            /// </summary>
            /// <param name="examId">Exam ID.</param>
            /// <param name="subject">Subject.</param>
            /// <param name="semester">Semester.</param>
            public Exam(int examId, Subject subject, Semester semester)
            {
                ExamId = examId;
                Subject = subject;
                Semester = semester;
            }

            /// <summary>
            /// Assigns an examiner to the exam.
            /// </summary>
            /// <param name="examiner">Examiner to assign.</param>
            public void AssignExaminer(Examiner examiner)
            {
                Examiner = examiner;
            }

            /// <summary>
            /// Sets the schedule for the exam.
            /// </summary>
            /// <param name="schedule">Exam schedule.</param>
            public void SetSchedule(ExamSchedule schedule)
            {
                Schedule = schedule;
            }

            /// <summary>
            /// Shows the details of the exam.
            /// </summary>
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

        /// <summary>
        /// Represents the Head of Department (HOD).
        /// </summary>
        public class HOD
        {
            /// <summary>
            /// Gets the HOD ID.
            /// </summary>
            public int HodId { get; }
            /// <summary>
            /// Gets the HOD name.
            /// </summary>
            public string Name { get; }
            /// <summary>
            /// Gets the department of the HOD.
            /// </summary>
            public Department Department { get; }

            private List<Exam> exams = new List<Exam>();

            /// <summary>
            /// Initializes a new HOD.
            /// </summary>
            /// <param name="hodId">HOD ID.</param>
            /// <param name="name">HOD name.</param>
            /// <param name="department">Department.</param>
            public HOD(int hodId, string name, Department department)
            {
                HodId = hodId;
                Name = name;
                Department = department;
            }

            /// <summary>
            /// Schedules an exam.
            /// </summary>
            /// <param name="exam">Exam to schedule.</param>
            public void ScheduleExam(Exam exam)
            {
                exams.Add(exam);
            }

            /// <summary>
            /// Assigns an examiner to an exam.
            /// </summary>
            /// <param name="exam">Exam.</param>
            /// <param name="examiner">Examiner.</param>
            public void AssignExaminer(Exam exam, Examiner examiner)
            {
                exam.AssignExaminer(examiner);
            }

            /// <summary>
            /// Lists all scheduled exams.
            /// </summary>
            public void ListExams()
            {
                foreach (var exam in exams)
                {
                    exam.ShowExamDetails();
                }
            }
        }

        /// <summary>
        /// Interface for serving platters.
        /// </summary>
        interface IPlatter
        {
            /// <summary>
            /// Serves the platter.
            /// </summary>
            void Serve();
            /// <summary>
            /// Gets the description of the platter.
            /// </summary>
            /// <returns>Description string.</returns>
            string GetDescription();
        }

        /// <summary>
        /// Represents a vegetarian platter.
        /// </summary>
        public class VegPlatter : IPlatter
        {
            /// <summary>
            /// Serves the vegetarian platter.
            /// </summary>
            public void Serve()
            {
                Console.WriteLine("Serving a vegetarian platter.");
            }

            /// <summary>
            /// Gets the description of the vegetarian platter.
            /// </summary>
            /// <returns>Description string.</returns>
            public string GetDescription()
            {
                return "A delicious vegetarian platter with assorted veggies.";
            }
        }

        /// <summary>
        /// Represents a non-vegetarian platter.
        /// </summary>
        public class NonVegPlatter : IPlatter
        {
            /// <summary>
            /// Serves the non-vegetarian platter.
            /// </summary>
            public void Serve()
            {
                Console.WriteLine("Serving a non-vegetarian platter.");
            }

            /// <summary>
            /// Gets the description of the non-vegetarian platter.
            /// </summary>
            /// <returns>Description string.</returns>
            public string GetDescription()
            {
                return "A delicious non-vegetarian platter with assorted meats.";
            }
        }

        /// <summary>
        /// Runs the demonstration of interfaces and organization structure.
        /// </summary>
        public static void Run()
        {
            // Create department and HOD
            Department cse = new Department(1, "Computer Science");
            HOD hod = new HOD(101, "Dr. Rao", cse);

            // Create subject and semester
            Subject math = new Subject("M101", "Mathematics", 4);
            Semester sem1 = new Semester("Semester 1", 2025);

            // Create exam
            Exam exam1 = new Exam(1, math, sem1);

            // Create examiner and schedule
            Examiner examiner = new Examiner(5001, "Dr. Smith", "Mathematics");
            ExamSchedule schedule = new ExamSchedule(
                new DateTime(2025, 12, 24),
                "10:00 AM - 1:00 PM",
                "Hall A"
            );

            // Schedule exam and assign examiner
            hod.ScheduleExam(exam1);
            hod.AssignExaminer(exam1, examiner);
            exam1.SetSchedule(schedule);
            hod.ListExams();

            // Demonstrate platters
            IPlatter vegPlatter = new VegPlatter();
            vegPlatter.Serve();
            Console.WriteLine(vegPlatter.GetDescription());

            IPlatter nonVegPlatter = new NonVegPlatter();
            nonVegPlatter.Serve();
            Console.WriteLine(nonVegPlatter.GetDescription());
        }
    }