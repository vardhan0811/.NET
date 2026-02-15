using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

#region Demo Classes

namespace Day14
{

public class Employee
{
    public int Id { get; set; } // Employee ID
    public string Name { get; set; } = ""; // Employee Name
    public decimal Salary { get; private set; } // Employee Salary

    private string secretCode = "X9Z"; // Employee Secret Code

    public Employee() { } // Default constructor

    public Employee(int id, string name, decimal salary) // Parameterized constructor
    {
        Id = id;
        Name = name;
        Salary = salary;
    }

    public void GiveRaise(decimal amount) // Method to give a raise
    {
        Salary += amount;
    }

    private string GetSecretCode() => secretCode; // Method to get secret code
}

[AttributeUsage(AttributeTargets.Property)]
public class RequiredAttribute : Attribute
{
    public string Message { get; }
    public RequiredAttribute(string message) => Message = message;
}

public class Student
{
    [Required("Student name is mandatory")]
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

public interface IPlugin
{
    string Key { get; }
    void Execute();
}

public class PdfPlugin : IPlugin
{
    public string Key => "pdf";
    public void Execute() => Console.WriteLine("PDF plugin executed");
}

#endregion

// Main program demonstrating C# Reflection and Attributes
public class Day14Reflections
{
    public static void Run()
    {
        Console.WriteLine("=== C# Reflection Master Demo ===\n");

        // 1. GET TYPE: Get the Type object for Employee
        Type empType = typeof(Employee);
        Console.WriteLine("Type: " + empType.FullName);

        // 2. CREATE OBJECT DYNAMICALLY: Create Employee instance using reflection
        object empObj = Activator.CreateInstance(empType, 101, "Arun", 45000m)!;

        // 3. READ PROPERTIES: List all public properties
        Console.WriteLine("\nProperties:");
        foreach (var p in empType.GetProperties())
            Console.WriteLine($"{p.PropertyType.Name} {p.Name}");

        // 4. INVOKE METHOD DYNAMICALLY: Call GiveRaise method
        MethodInfo raiseMethod = empType.GetMethod("GiveRaise")!;
        raiseMethod.Invoke(empObj, new object[] { 5000m });

        // 5. READ PROPERTY VALUE: Get Salary property value
        PropertyInfo salaryProp = empType.GetProperty("Salary")!;
        Console.WriteLine("\nSalary after raise: " + salaryProp.GetValue(empObj));

        // 6. ACCESS PRIVATE FIELD: Read private field value using reflection
        FieldInfo secretField = empType.GetField(
            "secretCode",
            BindingFlags.Instance | BindingFlags.NonPublic)!;
        Console.WriteLine("Private field value: " + secretField.GetValue(empObj));

        // 7. CALL PRIVATE METHOD: Invoke private method using reflection
        MethodInfo secretMethod = empType.GetMethod(
            "GetSecretCode",
            BindingFlags.Instance | BindingFlags.NonPublic)!;
        Console.WriteLine("Private method result: " + secretMethod.Invoke(empObj, null));

        // 8. CONSTRUCTOR INFO: List all constructors and their parameters
        Console.WriteLine("\nConstructors:");
        foreach (var c in empType.GetConstructors())
        {
            Console.WriteLine("Constructor:");
            foreach (var p in c.GetParameters())
                Console.WriteLine($"  {p.ParameterType.Name} {p.Name}");
        }

        // 9. CUSTOM ATTRIBUTE VALIDATION: Validate RequiredAttribute on Student
        Console.WriteLine("\nAttribute validation:");
        var student = new Student { Name = "", Age = 20 };
        Validate(student);

        // 10. ASSEMBLY SCANNING (PLUGIN): Find and execute all IPlugin implementations
        Console.WriteLine("\nPlugins found:");
        Assembly asm = Assembly.GetExecutingAssembly();
        var plugins = asm.GetTypes()
            .Where(t => typeof(IPlugin).IsAssignableFrom(t) && t.IsClass);
        foreach (var pt in plugins)
        {
            IPlugin plugin = (IPlugin)Activator.CreateInstance(pt)!;
            Console.WriteLine("Plugin key: " + plugin.Key);
            plugin.Execute();
        }

        Console.WriteLine("\n=== END ===");
    }

    // Validates properties marked with [Required] attribute
    static void Validate(object obj)
    {
        Type t = obj.GetType();
        foreach (var prop in t.GetProperties())
        {
            var attr = prop.GetCustomAttribute<RequiredAttribute>();
            if (attr == null) continue;
            object? value = prop.GetValue(obj);
            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
            {
                Console.WriteLine("Validation error: " + attr.Message);
                return;
            }
        }
        Console.WriteLine("Validation passed");
    }
}
}