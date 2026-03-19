using Microsoft.AspNetCore.Mvc;
using MVC1.Data;
using MVC1.Models;

namespace MVC1.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View(FakeDatabase.Departments.Where(d => d.IsActive));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            dept.Id = FakeDatabase.Departments.Count + 1;
            FakeDatabase.Departments.Add(dept);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var dept = FakeDatabase.Departments.FirstOrDefault(d => d.Id == id);
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            var existing = FakeDatabase.Departments.FirstOrDefault(d => d.Id == dept.Id);

            if (existing == null)
                return NotFound();

            existing.Name = dept.Name;

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var dept = FakeDatabase.Departments.FirstOrDefault(d => d.Id == id);

            if (dept == null)
                return NotFound();

            // Prevent delete if employees exist
            if (FakeDatabase.Employees.Any(e => e.DepartmentId == id && e.IsActive))
            {
                TempData["Error"] = "Cannot delete department with employees.";
                return RedirectToAction("Index");
            }

            dept.IsActive = false;

            return RedirectToAction("Index");
        }
    }
}