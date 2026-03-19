using EmployeeUnitTestApp.Models;
using EmployeeUnitTestApp.Repositories;
using EmployeeUnitTestApp.Services;
using Moq;
using Xunit;

namespace EmployeeTests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _repoMock;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repoMock.Object);
        }

        [Fact]
        public void GetEmployeeOrThrow_ValidId_ReturnsEmployee()
        {
            var employee = new Employee { Id = 1, Name = "John", IsActive = true };

            _repoMock.Setup(r => r.GetById(1)).Returns(employee);

            var result = _service.GetEmployeeOrThrow(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetEmployeeOrThrow_InvalidId_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _service.GetEmployeeOrThrow(-1));
        }

        [Fact]
        public void GetEmployeeOrThrow_NotFound_ThrowsException()
        {
            _repoMock.Setup(r => r.GetById(99)).Returns((Employee?)null);

            Assert.Throws<KeyNotFoundException>(() =>
                _service.GetEmployeeOrThrow(99));
        }

        [Fact]
        public void GetActiveEmployees_ReturnsOnlyActiveEmployees()
        {
            var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John", IsActive = true },
            new Employee { Id = 2, Name = "Mike", IsActive = false },
            new Employee { Id = 3, Name = "Sara", IsActive = true }
        };

            _repoMock.Setup(r => r.GetAll()).Returns(employees);

            var result = _service.GetActiveEmployees();

            Assert.Equal(2, result.Count);
        }
    }
}