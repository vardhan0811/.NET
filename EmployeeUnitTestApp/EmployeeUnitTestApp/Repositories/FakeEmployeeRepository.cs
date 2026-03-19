using System;
using System.Collections.Generic;
using System.Text;
using EmployeeUnitTestApp.Models;

namespace EmployeeUnitTestApp.Repositories
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employees = new List<Employee>()
        {
            new Employee { Id = 1, Name = "John", IsActive = true },
            new Employee { Id = 2, Name = "David", IsActive = false },
            new Employee { Id = 3, Name = "Sara", IsActive = true }
        };

        public Employee? GetById(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public List<Employee> GetAll()
        {
            return employees;
        }
    }
}
