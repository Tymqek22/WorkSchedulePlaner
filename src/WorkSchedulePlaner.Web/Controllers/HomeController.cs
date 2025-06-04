using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Web.Models;

namespace WorkSchedulePlaner.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorkScheduleRepository _workScheduleRepository;

        public HomeController(ILogger<HomeController> logger,IWorkScheduleRepository workScheduleRepository)
        {
            _logger = logger;
            _workScheduleRepository = workScheduleRepository;
        }

        public async Task<IActionResult> Index()
        {
            //temporary
            var schedules = await _workScheduleRepository.GetAllAsync();

			return View(schedules);
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
