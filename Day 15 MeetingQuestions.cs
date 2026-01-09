using System;
public class Employee
{
    public int Id { get; }
    public string? Name { get; }
    public Employee(int id, string? name)
    {
        this.Id = id;
        this.Name = name;
    }
}


public class Day15MeetingQuestions
{
    public static void Run(string[] args)
    {
        var employees = new List<Employee>
        {
            new Employee(101, "Mahy"),
            new Employee(106, "Mahia"),
            new Employee(108, "Mahio"),
            new Employee(111, "Mahii")
        };
        int secondHighestEmpId = employees
        .OrderByDescending(e => e.Id)
        .Skip(1)
        .First()
        .Id;

    Console.WriteLine(secondHighestEmpId);
    }
}
