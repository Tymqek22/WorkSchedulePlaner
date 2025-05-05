using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;

namespace WorkSchedulePlaner.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var schedule1 = new WorkSchedule
            {
                Id = 1,
                Title = "Grafik 1"
            };
            var schedule2 = new WorkSchedule
            {
                Id = 2,
                Title = "Grafik 2"
            };
            var schedule3 = new WorkSchedule
            {
                Id = 3,
                Title = "Grafik 3"
            };
            var schedule4 = new WorkSchedule
            {
                Id = 4,
                Title = "Grafik 4"
            };
            var schedule5 = new WorkSchedule
            {
                Id = 5,
                Title = "Grafik 5"
            };

            List<WorkSchedule> model = new();
            model.AddRange(schedule1,schedule2,schedule3,schedule4,schedule5);

			return View(model);
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
