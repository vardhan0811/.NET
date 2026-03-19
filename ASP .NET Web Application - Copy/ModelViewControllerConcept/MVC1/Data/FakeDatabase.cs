using MVC1.Models;

namespace MVC1.Data
{
    public static class FakeDatabase
    {
        public static List<Department> Departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR"},
            new Department { Id = 2, Name = "IT"},
            new Department { Id = 3, Name = "Finance"}
        };

        public static List<Employee> Employees = new List<Employee>();
    }
}
