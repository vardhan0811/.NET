using FlightSearchEngine.Data;
using FlightSearchEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightSearchEngine.Controllers
{
    public class FlightController : Controller
    {
        private readonly DatabaseHelper _db;

        // Constructor
        public FlightController(IConfiguration configuration)
        {
            _db = new DatabaseHelper(configuration);
        }

        // =========================
        // 1️⃣ GET: Index
        // =========================
        public async Task<IActionResult> Index()
        {
            var model = new SearchViewModel();

            var sources = await _db.GetSourcesAsync();
            var destinations = await _db.GetDestinationsAsync();

            model.SourceList = new SelectList(sources);
            model.DestinationList = new SelectList(destinations);

            return View(model);
        }

        // =========================
        // 2️⃣ POST: Search Flights Only
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlights(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns if validation fails
                model.SourceList = new SelectList(await _db.GetSourcesAsync());
                model.DestinationList = new SelectList(await _db.GetDestinationsAsync());

                return View("Index", model);
            }

            var results = await _db.SearchFlightsAsync(
                model.Source,
                model.Destination,
                model.NumberOfPersons
            );

            return View("Results", results);
        }

        // =========================
        // 3️⃣ POST: Search Flights + Hotels
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlightsWithHotels(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SourceList = new SelectList(await _db.GetSourcesAsync());
                model.DestinationList = new SelectList(await _db.GetDestinationsAsync());

                return View("Index", model);
            }

            var results = await _db.SearchFlightsWithHotelsAsync(
                model.Source,
                model.Destination,
                model.NumberOfPersons
            );

            return View("Results", results);
        }
    }
}