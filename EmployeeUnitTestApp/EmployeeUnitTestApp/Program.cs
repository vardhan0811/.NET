using EmployeeUnitTestApp.Repositories;
using EmployeeUnitTestApp.Services;
using EmployeeUnitTestApp.Models;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== UNIT TEST EXECUTION STARTED =====\n");

        IEmployeeRepository repo = new FakeEmployeeRepository();
        var service = new EmployeeService(repo);

        TestValidEmployee(service);
        TestInvalidId(service);
        TestEmployeeNotFound(service);
        TestActiveEmployees(service);

        Console.WriteLine("\n===== UNIT TEST EXECUTION FINISHED =====");
    }

    static void TestValidEmployee(EmployeeService service)
    {
        Console.WriteLine("Test 1: Valid Employee");

        try
        {
            var emp = service.GetEmployeeOrThrow(1);

            if (emp != null && emp.Id == 1)
                Console.WriteLine("PASS");
            else
                Console.WriteLine("FAIL");
        }
        catch (Exception ex)
        {
            Console.WriteLine("FAIL: " + ex.Message);
        }

        Console.WriteLine();
    }

    static void TestInvalidId(EmployeeService service)
    {
        Console.WriteLine("Test 2: Invalid ID");

        try
        {
            service.GetEmployeeOrThrow(-1);
            Console.WriteLine("FAIL: Exception not thrown");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("PASS");
        }
        catch (Exception ex)
        {
            Console.WriteLine("FAIL: Wrong exception " + ex.GetType().Name);
        }

        Console.WriteLine();
    }

    static void TestEmployeeNotFound(EmployeeService service)
    {
        Console.WriteLine("Test 3: Employee Not Found");

        try
        {
            service.GetEmployeeOrThrow(99);
            Console.WriteLine("FAIL: Exception not thrown");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("PASS");
        }
        catch (Exception ex)
        {
            Console.WriteLine("FAIL: Wrong exception " + ex.GetType().Name);
        }

        Console.WriteLine();
    }

    static void TestActiveEmployees(EmployeeService service)
    {
        Console.WriteLine("Test 4: Active Employees");

        var employees = service.GetActiveEmployees();

        if (employees.Count > 0 && employees.All(e => e.IsActive))
            Console.WriteLine("PASS");
        else
            Console.WriteLine("FAIL");

        Console.WriteLine();
    }
}