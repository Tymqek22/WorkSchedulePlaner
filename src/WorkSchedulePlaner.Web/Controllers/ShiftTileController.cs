using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.DTOs;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ShiftTileController : Controller
	{
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IRepository<EmployeeShift> _employeeShiftRepository;

		public ShiftTileController(
			IRepository<ShiftTile> shiftTileRepository,
			IRepository<EmployeeShift> employeeShiftRepository)
		{
			_shiftTileRepository = shiftTileRepository;
			_employeeShiftRepository = employeeShiftRepository;
		}

		public async Task<IActionResult> Create(DateTime date, int scheduleId,
			[FromServices] IQueryHandler<GetFromScheduleQuery,List<Employee>> handler)
		{
			ViewBag.Date = date.ToString("dd.MM.yyyy");
			ViewBag.ScheduleId = scheduleId;

			var query = new GetFromScheduleQuery(scheduleId);

			ViewBag.Employees = new SelectList(await handler.Handle(query),"Id","Name");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ShiftAssignmentVM viewModel,
			[FromServices] ICommandHandler<AssignShiftCommand,AssignShiftResult> handler)
		{
			var command = new AssignShiftCommand(
				viewModel.ShiftTile.Title,
				viewModel.ShiftTile.Description,
				viewModel.EmployeeWorkHours,
				viewModel.ShiftTile.Date,
				viewModel.ShiftTile.ScheduleId);

			var result = await handler.Handle(command);

			if (result != AssignShiftResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add shift."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule", new { id = command.ScheduleId });
		}

		public async Task<IActionResult> Update(int id,
			[FromServices] IQueryHandler<GetFromScheduleQuery,List<Employee>> handler)
		{
			//temporary

			var tile = await _shiftTileRepository.GetByIdAsync(id);

			var query = new GetFromScheduleQuery(tile.ScheduleId);
			ViewBag.Employees = new SelectList(await handler.Handle(query),"Id","Name");

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
		public async Task<IActionResult> Update(ShiftAssignmentVM viewModel,
			[FromServices] ICommandHandler<UpdateShiftCommand,UpdateShiftResult> handler)
		{
			var command = new UpdateShiftCommand(
				viewModel.ShiftTile.Id,
				viewModel.ShiftTile.Title,
				viewModel.ShiftTile.Description,
				viewModel.EmployeeWorkHours);

			var result = await handler.Handle(command);

			if (result != UpdateShiftResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add shift."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = viewModel.ShiftTile.ScheduleId });
		}

		public async Task<IActionResult> Delete(int tileId, int scheduleId,
			[FromServices] ICommandHandler<DeleteShiftCommand,DeleteShiftResult> handler)
		{
			var command = new DeleteShiftCommand(tileId);

			var result = await handler.Handle(command);

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
