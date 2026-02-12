using System;
using System.Collections.Generic;
using System.Linq;

namespace Day39
{
    // =============================
    // Base Interfaces
    // =============================
    public interface IStudent
    {
        int StudentId { get; }
        string? Name { get; }
        int Semester { get; }
    }

    public interface ICourse
    {
        string? CourseCode { get; }
        string? Title { get; }
        int MaxCapacity { get; }
        int Credits { get; }
    }

    // =============================
    // Generic Enrollment System
    // =============================
    public class EnrollmentSystem<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        // Dictionary: Course → List of students
        private Dictionary<TCourse, List<TStudent>> _enrollments = new();

        public bool EnrollStudent(TStudent student, TCourse course)
        {
            if (!_enrollments.ContainsKey(course))
                _enrollments[course] = new List<TStudent>();

            var students = _enrollments[course];

            // Rule 1: Capacity check
            if (students.Count >= course.MaxCapacity)
            {
                Console.WriteLine("Enrollment failed: Course at full capacity.");
                return false;
            }

            // Rule 2: Already enrolled check
            if (students.Any(s => s.StudentId == student.StudentId))
            {
                Console.WriteLine("Enrollment failed: Student already enrolled.");
                return false;
            }

            // Rule 3: Semester prerequisite check
            if (course is LabCourse lab)
            {
                if (student.Semester < lab.RequiredSemester)
                {
                    Console.WriteLine("Enrollment failed: Semester prerequisite not met.");
                    return false;
                }
            }

            students.Add(student);
            Console.WriteLine("Enrollment successful.");
            return true;
        }

        public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
        {
            if (_enrollments.ContainsKey(course))
                return _enrollments[course].AsReadOnly();

            return new List<TStudent>().AsReadOnly();
        }

        public IEnumerable<TCourse> GetStudentCourses(TStudent student)
        {
            return _enrollments
                .Where(e => e.Value.Any(s => s.StudentId == student.StudentId))
                .Select(e => e.Key);
        }

        public int CalculateStudentWorkload(TStudent student)
        {
            return GetStudentCourses(student)
                .Sum(c => c.Credits);
        }
    }

    // =============================
    // Specialized Implementations
    // =============================
    public class EngineeringStudent : IStudent
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public int Semester { get; set; }
        public string? Specialization { get; set; }
    }

    public class LabCourse : ICourse
    {
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int MaxCapacity { get; set; }
        public int Credits { get; set; }
        public string? LabEquipment { get; set; }
        public int RequiredSemester { get; set; }
    }

    // =============================
    // Generic GradeBook
    // =============================
    public class GradeBook<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        private Dictionary<(TStudent, TCourse), double> _grades = new();
        private EnrollmentSystem<TStudent, TCourse> _enrollmentSystem;

        public GradeBook(EnrollmentSystem<TStudent, TCourse> enrollmentSystem)
        {
            _enrollmentSystem = enrollmentSystem;
        }

        public void AddGrade(TStudent student, TCourse course, double grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentException("Grade must be between 0 and 100");

            if (!_enrollmentSystem.GetEnrolledStudents(course)
                .Any(s => s.StudentId == student.StudentId))
                throw new InvalidOperationException("Student not enrolled in course");

            _grades[(student, course)] = grade;
        }

        public double? CalculateGPA(TStudent student)
        {
            var studentGrades = _grades
                .Where(g => g.Key.Item1.StudentId == student.StudentId);

            if (!studentGrades.Any())
                return null;

            double totalPoints = 0;
            int totalCredits = 0;

            foreach (var entry in studentGrades)
            {
                var course = entry.Key.Item2;
                totalPoints += entry.Value * course.Credits;
                totalCredits += course.Credits;
            }

            return totalPoints / totalCredits;
        }

        public (TStudent student, double grade)? GetTopStudent(TCourse course)
        {
            var courseGrades = _grades
                .Where(g => g.Key.Item2.Equals(course));

            if (!courseGrades.Any())
                return null;

            var top = courseGrades
                .OrderByDescending(g => g.Value)
                .First();

            return (top.Key.Item1, top.Value);
        }
    }

    // =============================
    // QUESTION CLASS
    // =============================
    public class UniversityCourseRegistrationSystem
    {
        public static void Run()
        {
        var enrollment = new EnrollmentSystem<EngineeringStudent, LabCourse>();
        var gradeBook = new GradeBook<EngineeringStudent, LabCourse>(enrollment);

        var students = new List<EngineeringStudent>();
        var courses = new List<LabCourse>();

        // =============================
        // DEFAULT HARDCODED DATA
        // =============================

        students.Add(new EngineeringStudent
        {
            StudentId = 1,
            Name = "Michael",
            Semester = 3,
            Specialization = "Computer Science"
        });

        students.Add(new EngineeringStudent
        {
            StudentId = 2,
            Name = "Elijah",
            Semester = 5,
            Specialization = "Electronics"
        });

        courses.Add(new LabCourse
        {
            CourseCode = "CS301",
            Title = "Data Structures Lab",
            MaxCapacity = 3,
            Credits = 4,
            RequiredSemester = 3,
            LabEquipment = "Computers"
        });

        courses.Add(new LabCourse
        {
            CourseCode = "EC501",
            Title = "Microprocessor Lab",
            MaxCapacity = 2,
            Credits = 3,
            RequiredSemester = 5,
            LabEquipment = "Microcontroller Kits"
        });

        bool exit = false;

        while (!exit)
{
    Console.WriteLine("\n====== UNIVERSITY COURSE REGISTRATION SYSTEM ======");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. Add Course");
    Console.WriteLine("3. View All Students");
    Console.WriteLine("4. View All Courses");
    Console.WriteLine("5. Enroll Student");
    Console.WriteLine("6. Assign Grade");
    Console.WriteLine("7. Calculate GPA");
    Console.WriteLine("8. Show Top Student in Course");
    Console.WriteLine("9. Exit");

    Console.Write("Select an option: ");
    string userChoice = Console.ReadLine() ?? string.Empty;

    try
    {
        switch (userChoice)
        {
            case "1":

                Console.Write("Enter Student ID: ");
                string? studentIdInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(studentIdInput))
                {
                    Console.WriteLine("Invalid input. Student ID cannot be empty.");
                    break;
                }
                int studentId = int.Parse(studentIdInput);

                Console.Write("Enter Student Name: ");
                string studentName = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Semester: ");
                string? semesterInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(semesterInput))
                {
                    Console.WriteLine("Invalid input. Semester cannot be empty.");
                    break;
                }
                int studentSemester = int.Parse(semesterInput);

                Console.Write("Enter Specialization: ");
                string studentSpecialization = Console.ReadLine() ?? string.Empty;

                students.Add(new EngineeringStudent
                {
                    StudentId = studentId,
                    Name = studentName,
                    Semester = studentSemester,
                    Specialization = studentSpecialization
                });

                Console.WriteLine("Student added successfully.");
                break;

            case "2":

                Console.Write("Enter Course Code: ");
                string? courseCodeInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(courseCodeInput))
                {
                    Console.WriteLine("Invalid input. Course code cannot be empty.");
                    break;
                }
                string courseCode = courseCodeInput;

                Console.Write("Enter Course Title: ");
                string courseTitle = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Maximum Capacity: ");
                string? maxCapacityInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(maxCapacityInput))
                {
                    Console.WriteLine("Invalid input. Maximum capacity cannot be empty.");
                    break;
                }
                int maximumCapacity = int.Parse(maxCapacityInput);

                Console.Write("Enter Course Credits: ");
                string? courseCreditsInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(courseCreditsInput))
                {
                    Console.WriteLine("Invalid input. Course credits cannot be empty.");
                    break;
                }
                int courseCredits = int.Parse(courseCreditsInput);

                Console.Write("Enter Required Semester: ");
                string? requiredSemesterInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(requiredSemesterInput))
                {
                    Console.WriteLine("Invalid input. Required semester cannot be empty.");
                    break;
                }
                int requiredSemester = int.Parse(requiredSemesterInput);

                Console.Write("Enter Lab Equipment: ");
                string labEquipment = Console.ReadLine() ?? string.Empty;

                courses.Add(new LabCourse
                {
                    CourseCode = courseCode,
                    Title = courseTitle,
                    MaxCapacity = maximumCapacity,
                    Credits = courseCredits,
                    RequiredSemester = requiredSemester,
                    LabEquipment = labEquipment
                });

                Console.WriteLine("Course added successfully.");
                break;

            case "3":

                if (students.Count == 0)
                {
                    Console.WriteLine("No students available.");
                    break;
                }

                Console.WriteLine("\n--- Registered Students ---");
                foreach (var existingStudent in students)
                {
                    Console.WriteLine(
                        $"ID: {existingStudent.StudentId} | " +
                        $"Name: {existingStudent.Name} | " +
                        $"Semester: {existingStudent.Semester} | " +
                        $"Specialization: {existingStudent.Specialization}");
                }
                break;

            case "4":

                if (courses.Count == 0)
                {
                    Console.WriteLine("No courses available.");
                    break;
                }

                Console.WriteLine("\n--- Available Courses ---");
                foreach (var availableCourse in courses)
                {
                    Console.WriteLine(
                        $"Code: {availableCourse.CourseCode} | " +
                        $"Title: {availableCourse.Title} | " +
                        $"Credits: {availableCourse.Credits} | " +
                        $"Capacity: {availableCourse.MaxCapacity}");
                }
                break;

            case "5":

                if (students.Count == 0 || courses.Count == 0)
                {
                    Console.WriteLine("Students or courses are not available.");
                    break;
                }

                Console.WriteLine("\nAvailable Students:");
                foreach (var availableStudent in students)
                    Console.WriteLine($"ID: {availableStudent.StudentId} - {availableStudent.Name}");

                Console.Write("\nEnter Student ID to Enroll: ");
                string? enrollStudentIdInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(enrollStudentIdInput))
                {
                    Console.WriteLine("Invalid input. Student ID cannot be empty.");
                    break;
                }
                studentId = int.Parse(enrollStudentIdInput);

                Console.WriteLine("\nAvailable Courses:");
                foreach (var availableCourse in courses)
                    Console.WriteLine($"{availableCourse.CourseCode} - {availableCourse.Title}");

                Console.Write("\nEnter Course Code to Enroll: ");
                courseCode = Console.ReadLine() ?? string.Empty;

                EngineeringStudent? selectedStudent =
                    students.FirstOrDefault(s => s.StudentId == studentId);

                LabCourse? selectedCourse =
                    courses.FirstOrDefault(c => c.CourseCode == courseCode);

                if (selectedStudent != null && selectedCourse != null)
                    enrollment.EnrollStudent(selectedStudent, selectedCourse);
                else
                    Console.WriteLine("Invalid student or course selection.");

                break;

            case "6":

                Console.WriteLine("\nAvailable Students:");
                foreach (var availableStudent in students)
                    Console.WriteLine($"ID: {availableStudent.StudentId} - {availableStudent.Name}");

                Console.Write("Enter Student ID: ");
                string? gradeStudentIdInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(gradeStudentIdInput))
                {
                    Console.WriteLine("Invalid input. Student ID cannot be empty.");
                    break;
                }
                studentId = int.Parse(gradeStudentIdInput);

                Console.WriteLine("\nAvailable Courses:");
                foreach (var availableCourse in courses)
                    Console.WriteLine($"{availableCourse.CourseCode} - {availableCourse.Title}");

                Console.Write("Enter Course Code: ");
                string? inputCourseCode = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputCourseCode))
                {
                    Console.WriteLine("Invalid input. Course code cannot be empty.");
                    break;
                }
                courseCode = inputCourseCode;

                Console.Write("Enter Grade (0-100): ");
                string? gradeInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(gradeInput))
                {
                    Console.WriteLine("Invalid input. Grade cannot be empty.");
                    break;
                }
                double gradeValue = double.Parse(gradeInput);

                selectedStudent =
                    students.FirstOrDefault(s => s.StudentId == studentId);

                selectedCourse =
                    courses.FirstOrDefault(c => c.CourseCode == courseCode);

                if (selectedStudent != null && selectedCourse != null)
                    gradeBook.AddGrade(selectedStudent, selectedCourse, gradeValue);
                else
                    Console.WriteLine("Invalid student or course selection.");

                break;

            case "7":

                Console.WriteLine("\nAvailable Students:");
                foreach (var availableStudent in students)
                    Console.WriteLine($"ID: {availableStudent.StudentId} - {availableStudent.Name}");

                Console.Write("Enter Student ID: ");
                string? gpaStudentIdInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(gpaStudentIdInput))
                {
                    Console.WriteLine("Invalid input. Student ID cannot be empty.");
                    break;
                }
                studentId = int.Parse(gpaStudentIdInput);

                selectedStudent =
                    students.FirstOrDefault(s => s.StudentId == studentId);

                if (selectedStudent != null)
                {
                    double? calculatedGpa = gradeBook.CalculateGPA(selectedStudent);

                    Console.WriteLine(calculatedGpa.HasValue
                        ? $"Calculated GPA: {calculatedGpa.Value:F2}"
                        : "No grades available for this student.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }

                break;

            case "8":

                Console.WriteLine("\nAvailable Courses:");
                foreach (var availableCourse in courses)
                    Console.WriteLine($"{availableCourse.CourseCode} - {availableCourse.Title}");

                Console.Write("Enter Course Code: ");
                string? topCourseCodeInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(topCourseCodeInput))
                {
                    Console.WriteLine("Invalid input. Course code cannot be empty.");
                    break;
                }
                courseCode = topCourseCodeInput;

                selectedCourse =
                    courses.FirstOrDefault(c => c.CourseCode == courseCode);

                if (selectedCourse != null)
                {
                    var topPerformer = gradeBook.GetTopStudent(selectedCourse);

                    if (topPerformer.HasValue)
                        Console.WriteLine(
                            $"Top Student: {topPerformer.Value.student.Name} - {topPerformer.Value.grade}");
                    else
                        Console.WriteLine("No grades available for this course.");
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }

                break;

            case "9":
                exit = true;
                Console.WriteLine("Exiting the system...");
                break;

            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }
    catch (Exception exception)
    {
        Console.WriteLine($"Error: {exception.Message}");
    }
}
            
        }
    }
}
