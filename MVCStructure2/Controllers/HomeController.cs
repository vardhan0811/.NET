using Microsoft.AspNetCore.Mvc;
using MVCStructure2.Models;
using System.Diagnostics;

namespace MVCStructure2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult newActionMethod()
        {
            return View();
        }

        [HttpPost]
        public IActionResult newActionMethod(int a, int b)
        {
            int result = a / b;

            ViewBag.A = a;
            ViewBag.B = b;
            ViewBag.Result = result;

            return View();
        }

        public IActionResult GlobalExceptionalHandler()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
