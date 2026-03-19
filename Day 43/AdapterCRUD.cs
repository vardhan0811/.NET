using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;

class AdapterCRUD
{
    static string connectionString = @"Server=.\SQLEXPRESS;
                    Database=TrainingDB;
                    Trusted_Connection=True;
                    TrustServerCertificate=True;";

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== Employee Management (Disconnected) =====");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Display");
            Console.WriteLine("5. LINQ Display (Faster)");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Insert();
                    break;
                case 2:
                    Update();
                    break;
                case 3:
                    Delete();
                    break;
                case 4:
                    Display();
                    break;
                case 5:
                    LinqDisplay();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void Insert()
    {
        Console.Write("Enter Name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter Department: ");
        string dept = Console.ReadLine();

        Console.Write("Enter Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Invalid salary.");
            return;
        }

        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        DataRow row = table.NewRow();
        row["FullName"] = name;
        row["Department"] = dept;
        row["Salary"] = salary;

        table.Rows.Add(row);
        adapter.Update(ds, "Employees");

        Console.WriteLine("Insert successful.");
    }

    static void Update()
    {
        Console.Write("Enter Employee ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter New Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Department: ");
        string dept = Console.ReadLine();

        Console.Write("Enter New Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Invalid salary.");
            return;
        }

        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        bool found = false;

        foreach (DataRow row in table.Rows)
        {
            if ((int)row["EmployeeId"] == id)
            {
                row["FullName"] = name;
                row["Department"] = dept;
                row["Salary"] = salary;
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        adapter.Update(ds, "Employees");
        Console.WriteLine("Update successful.");
    }

    static void Delete()
    {
        Console.Write("Enter Employee ID to Delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        bool found = false;

        foreach (DataRow row in table.Rows)
        {
            if ((int)row["EmployeeId"] == id)
            {
                row.Delete();
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        adapter.Update(ds, "Employees");
        Console.WriteLine("Delete successful.");
    }

    static void Display()
    {
        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        Console.WriteLine("\n--- Employee List ---");

        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"{row["EmployeeId"]} | {row["FullName"]} | {row["Department"]} | {row["Salary"]}");
        }
    }

    static void LinqDisplay()
    {
        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];


        // Convert DataTable → LINQ compatible collection
        var rows = table.AsEnumerable();

        Console.WriteLine("\n========= LINQ ANALYTICS =========");

        // ===========================
        // 3️⃣ HIGH SALARY FILTER + ORDER
        // ===========================

        Console.WriteLine("\n--- High Salary Employees (>50000) Sorted ---");

        var highSalary = rows
            .Where(r => r.Field<decimal>("Salary") > 50000)
            .OrderByDescending(r => r.Field<decimal>("Salary"))  // Highest first
            .ThenBy(r => r.Field<string>("FullName"))
            .Select(r => new
            {
                Name = r.Field<string>("FullName"),
                Dept = r.Field<string>("Department"),
                Salary = r.Field<decimal>("Salary")
            });

        foreach (var e in highSalary)
        {
            Console.WriteLine($"{e.Name} | {e.Dept} | {e.Salary}");
        }



        // 2️⃣ Only Names (Projection)
        Console.WriteLine("\n--- Only Employee Names ---");

        var names = rows.Select(r => r.Field<string>("FullName"));

        foreach (var name in names)
            Console.WriteLine(name);


        // ===========================
        // 2️⃣ GROUPING + SORTING
        // ===========================

        Console.WriteLine("\n--- Grouped By Department (Sorted Inside Group) ---");

        var grouped = rows
            .OrderBy(r => r.Field<string>("Department"))   // Sort Departments First
            .ThenBy(r => r.Field<decimal>("Salary"))       // Sort Within Dept By Salary
            .ThenBy(r => r.Field<string>("FullName"))      // If salary same → sort by name
            .GroupBy(r => r.Field<string>("Department"));  // Now group

        foreach (var deptGroup in grouped)
        {
            Console.WriteLine($"\nDepartment: {deptGroup.Key}");

            foreach (var emp in deptGroup)
            {
                Console.WriteLine($"   {emp.Field<string>("FullName")} - {emp.Field<decimal>("Salary")}");
            }
        }

        // 4️⃣ Average Salary (Aggregation)
        Console.WriteLine("\n--- Average Salary ---");

        var avgSalary = rows.Average(r => r.Field<decimal>("Salary"));

        Console.WriteLine($"Average Salary: {avgSalary}");

        // ===========================
        // 1️⃣ ORDERING (Global Sort)
        // ===========================

        Console.WriteLine("\n--- Employees Ordered By Salary, Then Name ---");

        var orderedEmployees = rows
            .OrderBy(r => r.Field<decimal>("Salary"))      // Primary Sort
            .ThenBy(r => r.Field<string>("FullName"));     // Secondary Sort

        foreach (var emp in orderedEmployees)
        {
            Console.WriteLine($"{emp.Field<int>("EmployeeId")} | {emp.Field<string>("FullName")} | {emp.Field<string>("Department")} | {emp.Field<decimal>("Salary")}");
        }
    }

}
