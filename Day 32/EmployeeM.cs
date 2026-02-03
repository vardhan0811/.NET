using System;
using System.Collections.Generic;
using System.Linq;

namespace Day32
{
// Employee class represents an employee with basic details
public class Employee
{
    public string EmployeeId { get; set; }      // Unique employee ID
    public string Name { get; set; }            // Employee name
    public string Department { get; set; }      // Department name
    public double Salary { get; set; }          // Employee salary
    public DateTime JoiningDate { get; set; }   // Date of joining

    // Constructor to initialize employee details
    public Employee(string employeeId, string name, string department, double salary, DateTime joiningDate)
    {
        EmployeeId = employeeId;
        Name = name;
        Department = department;
        Salary = salary;
        JoiningDate = joiningDate;
    }
}

// HRManager class manages employee operations
public class HRManager
{
    private List<Employee> employees = new List<Employee>(); // List to store employees
    private int employeeCounter = 1;                         // Counter for generating employee IDs

    // Adds a new employee to the list
    public void AddEmployee(string name, string dept, double salary)
    {
        string empId = $"E{employeeCounter:D3}"; // Generate employee ID like E001, E002, etc.
        employeeCounter++;
        employees.Add(new Employee(empId, name, dept, salary, DateTime.Now));
    }

    // Groups employees by their department and returns a sorted dictionary
    public SortedDictionary<string, List<Employee>> GroupEmployeesByDepartment()
    {
        var grouped = new SortedDictionary<string, List<Employee>>();
        foreach (var emp in employees)
        {
            if (!grouped.ContainsKey(emp.Department))
                grouped[emp.Department] = new List<Employee>();
            grouped[emp.Department].Add(emp);
        }
        return grouped;
    }

    // Calculates the total salary for a given department
    public double CalculateDepartmentSalary(string department)
    {
        return employees.Where(e => e.Department == department)
                        .Sum(e => e.Salary);
    }

    // Returns a list of employees who joined after a specific date
    public List<Employee> GetEmployeesJoinedAfter(DateTime date)
    {
        return employees.Where(e => e.JoiningDate > date).ToList();
    }

    // Returns all employees
    public List<Employee> GetAllEmployees()
    {
        return employees;
    }
}

// EmployeeM class contains the main menu and user interaction logic
public class EmployeeM
{
    public static void Run()
    {
        HRManager hr = new HRManager(); // Create HRManager instance
        bool exit = false;

        while (!exit)
        {
            // Display menu options
            Console.WriteLine("\n--- Employee Management Menu ---");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. List All Employees");
            Console.WriteLine("3. Group Employees By Department");
            Console.WriteLine("4. Calculate Department Salary");
            Console.WriteLine("5. List Employees Joined After Date");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    // Add a new employee
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine() ?? string.Empty;
                    Console.Write("Enter Department: ");
                    string dept = Console.ReadLine() ?? string.Empty;
                    Console.Write("Enter Salary: ");
                    double salary;
                    while (!double.TryParse(Console.ReadLine(), out salary))
                    {
                        Console.Write("Invalid input. Enter Salary: ");
                    }
                    hr.AddEmployee(name, dept, salary);
                    Console.WriteLine("Employee added.");
                    break;

                case "2":
                    // List all employees
                    var allEmps = hr.GetAllEmployees();
                    Console.WriteLine("\nAll Employees:");
                    foreach (var emp in allEmps)
                    {
                        Console.WriteLine($"{emp.EmployeeId} | {emp.Name} | {emp.Department} | {emp.Salary:C} | {emp.JoiningDate}");
                    }
                    break;

                case "3":
                    // Group employees by department
                    var grouped = hr.GroupEmployeesByDepartment();
                    Console.WriteLine("\nEmployees Grouped By Department:");
                    foreach (var deptGroup in grouped)
                    {
                        Console.WriteLine($"\nDepartment: {deptGroup.Key}");
                        foreach (var emp in deptGroup.Value)
                        {
                            Console.WriteLine($"  {emp.EmployeeId} | {emp.Name} | {emp.Salary:C} | {emp.JoiningDate}");
                        }
                    }
                    break;

                case "4":
                    // Calculate total salary for a department
                    Console.Write("Enter Department: ");
                    string dep = Console.ReadLine() ?? string.Empty;
                    double totalSalary = hr.CalculateDepartmentSalary(dep);
                    Console.WriteLine($"Total Salary for {dep}: {totalSalary:C}");
                    break;

                case "5":
                    // List employees who joined after a specific date
                    Console.Write("Enter Date (yyyy-MM-dd): ");
                    DateTime date;
                    while (!DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        Console.Write("Invalid date. Enter Date (yyyy-MM-dd): ");
                    }
                    var joinedAfter = hr.GetEmployeesJoinedAfter(date);
                    Console.WriteLine($"\nEmployees Joined After {date:yyyy-MM-dd}:");
                    foreach (var emp in joinedAfter)
                    {
                        Console.WriteLine($"{emp.EmployeeId} | {emp.Name} | {emp.Department} | {emp.Salary:C} | {emp.JoiningDate}");
                    }
                    break;

                case "6":
                    // Exit the program
                    exit = true;
                    break;

                default:
                    // Handle invalid menu option
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
        Console.WriteLine("Exiting Employee Management.");
    }
}
}