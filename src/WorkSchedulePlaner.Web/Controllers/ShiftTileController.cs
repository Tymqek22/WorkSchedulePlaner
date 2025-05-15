using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTO;
using WorkSchedulePlaner.Application.ShiftTiles.AssignShift;
using WorkSchedulePlaner.Application.ShiftTiles.DeleteShift;
using WorkSchedulePlaner.Application.ShiftTiles.UpdateShift;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ShiftTileController : Controller
	{
		private readonly IRepository<Employee> _repository;
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IRepository<EmployeeShift> _employeeShiftRepository;
		private readonly AssignShift _assignShift;
		private readonly DeleteShift _deleteShift;
		private readonly UpdateShift _updateShift;

		public ShiftTileController(IRepository<Employee> repository, IRepository<ShiftTile> shiftTileRepository,
			IRepository<EmployeeShift> employeeShiftRepository, AssignShift assignShift, DeleteShift deleteShift, 
			UpdateShift updateShift)
		{
			_repository = repository;
			_shiftTileRepository = shiftTileRepository;
			_employeeShiftRepository = employeeShiftRepository;
			_assignShift = assignShift;
			_deleteShift = deleteShift;
			_updateShift = updateShift;
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

		public async Task<IActionResult> Update(int id)
		{
			//temporary
			ViewBag.Employees = new SelectList(await _repository.GetAllAsync(),"Id","Name");

			var tile = await _shiftTileRepository.GetByIdAsync(id);
			var employeeShifts = await _employeeShiftRepository.GetAsync(es => es.ShiftTileId == tile.Id);

			var employeeWorkHours = new List<EmployeeWorkHoursDto>();

			foreach (var employeeShift in employeeShifts) {

				employeeWorkHours.Add(new EmployeeWorkHoursDto
				{
					EmployeeId = employeeShift.EmployeeId,
					StartTime = employeeShift.StartTime,
					EndTime = employeeShift.EndTime
				});
			}

			var viewModel = new ShiftAssignmentVM
			{
				ShiftTile = tile,
				EmployeeWorkHours = employeeWorkHours
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Update(ShiftAssignmentVM viewModel)
		{
			var request = new UpdateShiftRequest(
				viewModel.ShiftTile.Id,
				viewModel.EmployeeWorkHours,
				viewModel.ShiftTile.Title,
				viewModel.ShiftTile.Description);

			var result = await _updateShift.Handle(request);

			if (result != UpdateShiftResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add shift."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = viewModel.ShiftTile.ScheduleId });
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
