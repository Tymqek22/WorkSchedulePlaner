using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Queries.GetTileById;
using WorkSchedulePlaner.Web.Mappers;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	[Authorize]
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
		public async Task<IActionResult> Create(int scheduleId,ShiftTileDto dto)
		{
			var command = new CreateShiftCommand(
				dto.Title,
				dto.Description,
				dto.Date,
				dto.Shifts,
				scheduleId);

			var result = await _commandDispatcher.Dispatch<CreateShiftCommand,Result>((CreateShiftCommand)command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule", new { id = scheduleId });
		}

		public async Task<IActionResult> Update(int scheduleId,int tileId)
		{
			var query1 = new GetTileByIdQuery(scheduleId,tileId);
			var shiftTile = await _queryDispatcher.Dispatch<GetTileByIdQuery,ShiftTileDto>(query1);

			var query2 = new GetFromScheduleQuery(scheduleId);
			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query2);
			ViewBag.Employees = new SelectList(employees,"Id","Name");

			var viewModel = ShiftTileMapper.MapToVM(shiftTile,scheduleId);

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int scheduleId,ShiftTileDto dto)
		{
			ViewBag.ScheduleId = scheduleId;

			var command = new UpdateShiftCommand(
				dto.Id,
				dto.Title,
				dto.Description,
				dto.Shifts,
				scheduleId);

			var result = await _commandDispatcher.Dispatch<UpdateShiftCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = scheduleId });
		}

		public async Task<IActionResult> Delete(int tileId, int scheduleId)
		{
			var command = new DeleteShiftCommand(scheduleId,tileId);

			var result = await _commandDispatcher.Dispatch<DeleteShiftCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule", new { id = scheduleId });
		}
	}
}
