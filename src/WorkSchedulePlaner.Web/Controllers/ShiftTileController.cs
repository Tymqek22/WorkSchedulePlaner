using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Queries.GetTileById;
using WorkSchedulePlaner.Web.Models;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ShiftTileController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;

		public ShiftTileController(
			ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		public async Task<IActionResult> Create(DateTime date, int scheduleId)
		{
			ViewBag.Date = date.ToString("dd.MM.yyyy");
			ViewBag.ScheduleId = scheduleId;

			var query = new GetFromScheduleQuery(scheduleId);
			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query);

			ViewBag.Employees = new SelectList(employees,"Id","Name");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(int scheduleId,ShiftTileDto viewModel)
		{
			var command = new AssignShiftCommand(
				viewModel.Title,
				viewModel.Description,
				viewModel.Shifts,
				viewModel.Date,
				scheduleId);

			var result = await _commandDispatcher.Dispatch<AssignShiftCommand,AssignShiftResult>(command);

			if (result != AssignShiftResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add shift."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule", new { id = scheduleId });
		}

		public async Task<IActionResult> Update(int scheduleId,int tileId)
		{
			var query1 = new GetTileByIdQuery(tileId);
			var shiftTile = await _queryDispatcher.Dispatch<GetTileByIdQuery,ShiftTileDto>(query1);

			ViewBag.ScheduleId = scheduleId;

			var query2 = new GetFromScheduleQuery(scheduleId);
			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query2);
			ViewBag.Employees = new SelectList(employees,"Id","Name");

			return View(shiftTile);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int scheduleId,ShiftTileDto viewModel)
		{
			ViewBag.ScheduleId = scheduleId;

			var command = new UpdateShiftCommand(
				viewModel.Id,
				viewModel.Title,
				viewModel.Description,
				viewModel.Shifts);

			var result = await _commandDispatcher.Dispatch<UpdateShiftCommand,UpdateShiftResult>(command);

			if (result != UpdateShiftResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add shift."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = scheduleId });
		}

		public async Task<IActionResult> Delete(int tileId, int scheduleId)
		{
			var command = new DeleteShiftCommand(tileId);

			var result = await _commandDispatcher.Dispatch<DeleteShiftCommand,DeleteShiftResult>(command);

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
