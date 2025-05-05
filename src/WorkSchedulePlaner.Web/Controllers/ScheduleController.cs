using Microsoft.AspNetCore.Mvc;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ScheduleController : Controller
	{
		public IActionResult Details(int id)
		{
			ViewBag.ScheduleId = id;

			return View();
		}

		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Delete()
		{
			return View();
		}

		public IActionResult Update()
		{
			return View();
		}
	}
}
