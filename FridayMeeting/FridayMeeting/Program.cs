using System;
using System.Collections.Generic;
using System.Linq;

namespace FridayMeeting
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeesList = new List<Employee>
            {
                new Employee() {EmployeeID = 1001,FirstName = "Malcolm",LastName = "Daruwalla",Title = "Manager",DOB = DateTime.Parse("1984-01-02"),DOJ = DateTime.Parse("2011-08-09"),City = "Mumbai"},
                new Employee() {EmployeeID = 1002,FirstName = "Asdin",LastName = "Dhalla",Title = "AsstManager",DOB = DateTime.Parse("1984-08-20"),DOJ = DateTime.Parse("2012-7-7"),City = "Mumbai"},
                new Employee() {EmployeeID = 1003,FirstName = "Madhavi",LastName = "Oza",Title = "Consultant",DOB = DateTime.Parse("1987-11-14"),DOJ = DateTime.Parse("2105-12-04"),City = "Pune"},
                new Employee() {EmployeeID = 1004,FirstName = "Saba",LastName = "Shaikh",Title = "SE",DOB = DateTime.Parse("6/3/1990"),DOJ = DateTime.Parse("2/2/2016"),City = "Pune"},
                new Employee() {EmployeeID = 1005,FirstName = "Nazia",LastName = "Shaikh",Title = "SE",DOB = DateTime.Parse("3/8/1991"),DOJ = DateTime.Parse("2/2/2016"),City = "Mumbai"},
                new Employee() {EmployeeID = 1006,FirstName = "Suresh",LastName = "Pathak",Title = "Consultant",DOB = DateTime.Parse("11/7/1989"),DOJ = DateTime.Parse("8/8/2014"),City = "Chennai"},
                new Employee() {EmployeeID = 1007,FirstName = "Vijay",LastName = "Natrajan",Title = "Consultant",DOB = DateTime.Parse("12/2/1989"),DOJ = DateTime.Parse("6/1/2015"),City = "Mumbai"},
                new Employee() {EmployeeID = 1008,FirstName = "Rahul",LastName = "Dubey",Title = "Associate",DOB = DateTime.Parse("11/11/1993"),DOJ = DateTime.Parse("11/6/2014"),City = "Chennai"},
                new Employee() {EmployeeID = 1009,FirstName = "Amit",LastName = "Mistry",Title = "Associate",DOB = DateTime.Parse("8/12/1992"),DOJ = DateTime.Parse("12/3/2014"),City = "Chennai"},
                new Employee() {EmployeeID = 1010,FirstName = "Sumit",LastName = "Shah",Title = "Manager",DOB = DateTime.Parse("4/12/1991"),DOJ = DateTime.Parse("1/2/2016"),City = "Pune"},
            };

            //Display all employees
            Console.WriteLine("All Employees List:");
            var allEmployeesList = employeesList;
            foreach (var emp in allEmployeesList)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City}");
            }

            //Display all employees whose location is not "Mumbai"
            Console.WriteLine("\nEmployees whose location is not Mumbai:");
            var notMumbaiLocation = employeesList.Where(e => e.City != "Mumbai");
            foreach (var emp in notMumbaiLocation)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City}");
            }

            //Display all employees whose title is "AsstManager"
            Console.WriteLine("\nEmployees whose title is AsstManager:");
            var asstManagerTitle = employeesList.Where(e => e.Title == "AsstManager");
            foreach (var emp in asstManagerTitle)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City}");
            }

            //Display all employees whose LastName start with "S"
            Console.WriteLine("\nEmployees whose LastName starts with S:");
            var lastNameStartsWithS = employeesList.Where(e => e.LastName.StartsWith("S"));
            foreach (var emp in lastNameStartsWithS)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City}");
            }

            //Display all employees who have joined before 1/1/2015
            Console.WriteLine("\nEmployees who have joined before 1/1/2015:");
            var joinedBefore2015 = employeesList.Where(e => e.DOJ < DateTime.Parse("1/1/2015"));
            foreach (var emp in joinedBefore2015)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City} - {emp.DOJ}");
            }

            //Display all employees whose date of birth is after 1/1/1990
            Console.WriteLine("\nEmployees whose date of birth is after 1/1/1990:");
            var dobAfter1990 = employeesList.Where(e => e.DOB > DateTime.Parse("1/1/1990"));
            foreach (var emp in dobAfter1990)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City} - {emp.DOB}");
            }

            //Display all employees whose designation is Consultant and Associate
            Console.WriteLine("\nEmployees whose designation is Consultant and Associate:");
            foreach (var emp in employeesList)
            {
                if (emp.Title == "Consultant" || emp.Title == "Associate")
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City}");
                }
            }

            //Display total number of employees
            Console.WriteLine("\nTotal number of employees:");
            var totalEmployees = employeesList.Count();
            Console.WriteLine(totalEmployees);

            //Display total number of employees belonging to "Chennai" location
            Console.WriteLine("\nTotal number of employees belonging to Chennai location:");
            var chennaiEmployees = employeesList.Count(e => e.City == "Chennai");
            Console.WriteLine(chennaiEmployees);

            //Display highest employee id from employees list
            Console.WriteLine("\nHighest employee id from employees list:");
            var highestEmployeeId = employeesList.Max(e => e.EmployeeID);
            Console.WriteLine(highestEmployeeId);

            //Display total number of employees who have joined after 1/1/2015
            Console.WriteLine("\nEmployees who have joined after 1/1/2015:");
            var joinedAfter2015 = employeesList.Count(e => e.DOJ > DateTime.Parse("1/1/2015"));
            Console.WriteLine(joinedAfter2015);

            //Display total number of employees whose designation is not "Associate"
            Console.WriteLine("\nTotal number of employees whose designation is not Associate:");
            var notAssociate = employeesList.Count(e => e.Title != "Associate");
            Console.WriteLine(notAssociate);

            //Display total number of employee based on City
            Console.WriteLine("\nTotal number of employee based on City:");
            var employeesByCity = employeesList.GroupBy(e => e.City).Select(group => new { City = group.Key, Count = group.Count() });
            foreach (var cityGroup in employeesByCity)
            {
                Console.WriteLine($"{cityGroup.City}: {cityGroup.Count}");
            }

            //Display total number of employee based on City and Title
            Console.WriteLine("\nTotal number of employee based on City and Title:");
            var employeesByCityAndTitle = employeesList.GroupBy(e => new { e.City, e.Title }).Select(group => new { City = group.Key.City, Title = group.Key.Title, Count = group.Count() });
            foreach (var group in employeesByCityAndTitle)
            {
                Console.WriteLine($"{group.City} - {group.Title}: {group.Count}");
            }

            //Display total number of employees who is youngest in the list
            Console.WriteLine("\nTotal number of employees who is youngest in the list:");
            var youngestDOB = employeesList.Max(e => e.DOB);
            var youngestEmployees = employeesList.Where(e => e.DOB == youngestDOB);
            foreach (var emp in youngestEmployees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title} - {emp.City} - {emp.DOB}");
            }
        }
    }
}
