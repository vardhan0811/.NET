using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC1.Data;
using MVC1.Models;
using System.Linq;

namespace MVC1.Controllers
{
    public class EmployeeController : Controller
    {
        // LIST
        public IActionResult Index(string search, int? departmentId)
        {
            var employees = FakeDatabase.Employees.Where(e => e.IsActive);

            if (!string.IsNullOrEmpty(search))
                employees = employees.Where(e => e.Name != null && e.Name.Contains(search));

            if (departmentId.HasValue)
                employees = employees.Where(e => e.DepartmentId == departmentId);

            ViewBag.Departments = FakeDatabase.Departments;

            return View(employees.ToList());

        }

        // CREATE - GET
        public IActionResult Create()
        {
            ViewBag.DepartmentList = new SelectList(
                FakeDatabase.Departments, "Id", "Name");

            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DepartmentList = new SelectList(
                    FakeDatabase.Departments, "Id", "Name");
                return View(emp);
            }

            emp.Id = FakeDatabase.Employees.Count + 1;
            FakeDatabase.Employees.Add(emp);

            return RedirectToAction("Index");
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var emp = FakeDatabase.Employees.FirstOrDefault(e => e.Id == id);

            if (emp == null)
                return NotFound();

            ViewBag.DepartmentList = new SelectList(
                FakeDatabase.Departments, "Id", "Name", emp.DepartmentId);

            return View(emp);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            if (!ModelState.IsValid)
                return View(emp);

            var existing = FakeDatabase.Employees.FirstOrDefault(e => e.Id == emp.Id);

            if (existing == null)
                return NotFound();

            existing.Name = emp.Name;
            existing.Email = emp.Email;
            existing.Salary = emp.Salary;
            existing.DepartmentId = emp.DepartmentId;

            return RedirectToAction("Index");
        }

        // SOFT DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var emp = FakeDatabase.Employees.FirstOrDefault(e => e.Id == id);

            if (emp == null)
                return NotFound();

            emp.IsActive = false;

            return RedirectToAction("Index");
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var emp = FakeDatabase.Employees.FirstOrDefault(e => e.Id == id);

            if (emp == null)
                return NotFound();

            var dept = FakeDatabase.Departments
                .FirstOrDefault(d => d.Id == emp.DepartmentId);

            ViewBag.DepartmentName = dept?.Name;

            return View(emp);
        }

        // GET
        public IActionResult CsvDemo()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CsvDemo(string inputValues)
        {
            if (!string.IsNullOrWhiteSpace(inputValues))
            {
                var items = inputValues
                                .Split(',')
                                .Select(x => x.Trim())
                                .Where(x => !string.IsNullOrEmpty(x))
                                .ToList();

                var mainDictionary = new Dictionary<string, List<Dictionary<string, string>>>();

                var list = new List<Dictionary<string, string>>();

                foreach (var item in items)
                {
                    var innerDictionary = new Dictionary<string, string>();
                    innerDictionary["Name"] = item;
                    innerDictionary["Length"] = item.Length.ToString();

                    list.Add(innerDictionary);
                }

                mainDictionary["Departments"] = list;

                ViewData["ComplexResult"] = mainDictionary;
            }

            return View();
        }
    }
}