using System;
using System.Collections.Generic;
using System.Text;
using EmployeeUnitTestApp.Models;
using EmployeeUnitTestApp.Repositories;

namespace EmployeeUnitTestApp.Services
{
    public sealed class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public Employee GetEmployeeOrThrow(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be positive.");

            var employee = _repo.GetById(id);

            if (employee is null)
                throw new KeyNotFoundException($"Employee with id {id} not found.");

            return employee;
        }

        public IReadOnlyList<Employee> GetActiveEmployees()
        {
            return _repo.GetAll()
                        .Where(e => e.IsActive)
                        .ToList();
        }
    }
}
