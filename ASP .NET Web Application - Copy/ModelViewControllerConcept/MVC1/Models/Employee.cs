using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC1.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public decimal Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        public List<SelectListItem>? Departments { get; set; }
    }
}