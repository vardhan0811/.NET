using System;
using System.Collections.Generic;

namespace Day19Generics
{
    // STUDENT HIERARCHY
    public abstract class Student
    {
        public int Id { get; set; }
        public abstract string GetStudentType();
    }

    public class UGStudent : Student
    {
        public int HighSchoolMark { get; set; }

        public override string GetStudentType()
        {
            return "UG Student";
        }
    }

    public class PGStudent : Student
    {
        public int UGMark { get; set; }

        public override string GetStudentType()
        {
            return "PG Student";
        }
    }

    // GENERIC CLASS
    public class MyGlobalType<T> where T : Student
    {
        private readonly List<T> _myCollection;

        public MyGlobalType()
        {
            _myCollection = new List<T>();
        }

        public void AddItem(T t)
        {
            _myCollection.Add(t);
        }

        public List<T> GetCollection()
        {
            return _myCollection;
        }

        public string GetDataType(T t)
        {
            return t.GetType().Name;
        }

        public string ActBasedOnType(T t)
        {
            return t.GetStudentType(); // polymorphism
        }
    }

    // MAIN + DELEGATES
    public class Day19Generics
    {
        public static void Run(string[] args)
        {
            Action<string> logger;

            if (DateTime.Now.Hour < 12)
            {
                logger = GoodMorning();
            }
            else
            {
                logger = GoodDay();
            }

            logger("Application Started");

            logger = ApplicationLogger();
            logger("System Ready");

            MyGlobalType<Student> myGlobal = new MyGlobalType<Student>();

            UGStudent ug = new UGStudent { Id = 1, HighSchoolMark = 85 };
            PGStudent pg = new PGStudent { Id = 2, UGMark = 78 };

            myGlobal.AddItem(ug);
            myGlobal.AddItem(pg);

            foreach (var student in myGlobal.GetCollection())
            {
                Console.WriteLine(
                    $"{myGlobal.GetDataType(student)} - {myGlobal.ActBasedOnType(student)}");
            }
        }

        private static Action<string> GoodMorning()
        {
            return message =>
            {
                Console.WriteLine("Good Morning");
            };
        }

        private static Action<string> GoodDay()
        {
            return message =>
            {
                Console.WriteLine("Good Day");
            };
        }

        private static Action<string> ApplicationLogger()
        {
            return message =>
            {
                Console.WriteLine($"[LOG]: {message} at {DateTime.Now}");
            };
        }
    }
}
