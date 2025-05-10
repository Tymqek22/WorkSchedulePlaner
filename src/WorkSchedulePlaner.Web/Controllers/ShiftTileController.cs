using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Application.ShiftTiles.AssignShift;
using WorkSchedulePlaner.Application.ShiftTiles.DeleteShift;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ShiftTileController : Controller
	{
		private readonly IRepository<Employee> _repository;
		private readonly AssignShift _assignShift;
		private readonly DeleteShift _deleteShift;

		public ShiftTileController(IRepository<Employee> repository, AssignShift assignShift,
			DeleteShift deleteShift)
		{
			_repository = repository;
			_assignShift = assignShift;
			_deleteShift = deleteShift;
		}

		public async Task<IActionResult> Create(DateTime date, int scheduleId)
		{
			ViewBag.Date = date.ToString("dd.MM.yyyy");
			ViewBag.ScheduleId = scheduleId;
			ViewBag.Employees = new SelectList(await _repository.GetAllAsync(),"Id","Name");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ShiftAssignmentVM viewModel)
		{
			var request = new AssignShiftRequest(
				viewModel.EmployeeWorkHours,
				viewModel.ShiftTile.Title,
				viewModel.ShiftTile.Description,
				viewModel.ShiftTile.Date,
				viewModel.ShiftTile.ScheduleId);

			var result = await _assignShift.Handle(request);

			if (result != AssignShiftResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add shift."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule", new { id = request.ScheduleId });
		}

		public IActionResult Update()
		{
			return View();
		}

		public async Task<IActionResult> Delete(int tileId, int scheduleId)
		{
			var request = new DeleteShiftRequest(tileId);

			var result = await _deleteShift.Handle(request);

			if (result != DeleteShiftResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot delete shift."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule", new { id = scheduleId });
		}
	}
}
