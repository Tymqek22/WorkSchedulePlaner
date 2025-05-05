using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Application.ShiftTiles.AssignShift;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ShiftTileController : Controller
	{
		private readonly IRepository<Employee> _repository;

		public ShiftTileController(IRepository<Employee> repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> Create(DateTime date, int scheduleId)
		{
			ViewBag.Date = date;
			ViewBag.ScheduleId = scheduleId;
			ViewBag.Employees = new SelectList(await _repository.GetAllAsync(),"Id","Name");

			return View();
		}

		[HttpPost]
		public IActionResult Create(ShiftAssignmentVM viewModel)
		{
			var request = new AssignShiftRequest(
				viewModel.EmployeeId,
				viewModel.ShiftTile.Title,
				viewModel.ShiftTile.Description,
				viewModel.ShiftTile.Date,
				viewModel.ShiftTile.ScheduleId,
				viewModel.StartTime,
				viewModel.EndTime);

			return RedirectToAction("Details","Schedule");
		}

		public IActionResult Update()
		{
			return View();
		}

		public IActionResult Delete()
		{
			return View();
		}
	}
}
