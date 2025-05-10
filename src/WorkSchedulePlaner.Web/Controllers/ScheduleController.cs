using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ScheduleController : Controller
	{
		private readonly IWorkScheduleRepository _repository;

		public ScheduleController(IWorkScheduleRepository repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> Details(int id)
		{
			//temporary
			var schedule = await _repository.GetWithIncludesAsync(id);

			DateTime startDay = DateTime.Today.AddDays(
				(int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
				(int)DateTime.Today.DayOfWeek);

			var dates = Enumerable
				.Range(0,7)
				.Select(i => startDay.AddDays(i))
				.ToList();

			var viewModel = new ScheduleDetailsVM
			{
				Schedule = schedule,
				Dates = dates
			};

			return View(viewModel);
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
