using System;
using System.Collections.Generic;
using System.Linq;

namespace Day32
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? GradeLevel { get; set; }
        public Dictionary<string, double> Subjects { get; set; }
        public Student()
        {
            Subjects = new Dictionary<string, double>();
        }
    }

    public class SchoolManager
    {
        private List<Student> students = new List<Student>();
        private int nextId = 1;

        public void AddStudent(string name, string gradeLevel)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            if(string.IsNullOrWhiteSpace(gradeLevel))
            {
                throw new ArgumentException("Grade level cannot be empty.");
            }

            Student student = new Student
            {
                StudentId = nextId++,
                Name = name,
                GradeLevel = gradeLevel
            };

            students.Add(student);
        }

        public void AddGrade(int studentId, string subject, double grade)
        {
            if(grade < 0 || grade > 100)
            {
                throw new ArgumentException("Grade must be between 0 and 100.");
            }

            Student? student = students.FirstOrDefault(s => s.StudentId == studentId);
            if(student == null)
            {
                throw new Exception("Student not found.");
            }

            student.Subjects[subject] = grade;
        }

        public SortedDictionary<string, List<Student>> GroupStudentsByGradeLevel()
        {
            return new SortedDictionary<string, List<Student>>(
                students.GroupBy(s => s.GradeLevel)
                        .ToDictionary(g => g.Key ?? "Unknown", g => g.ToList())
            );
        }
    }
}