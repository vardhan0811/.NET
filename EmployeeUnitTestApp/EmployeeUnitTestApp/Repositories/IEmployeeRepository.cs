using System;
using System.Collections.Generic;
using System.Text;
using EmployeeUnitTestApp.Models;

namespace EmployeeUnitTestApp.Repositories
{
    public interface IEmployeeRepository
    {
        Employee? GetById(int id);

        List<Employee> GetAll();
    }
}