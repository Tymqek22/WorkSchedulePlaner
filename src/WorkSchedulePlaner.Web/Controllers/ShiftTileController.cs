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
using WorkSchedulePlaner.Web.Requests;
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
			var employeesList = await GetEmployeesSelectListAsync(scheduleId);

			var formattedDate = DateOnly.FromDateTime(date).ToString("dd.MM.yyyy");

			var request = new UpsertAssignmentRequest
			{
				Date = DateOnly.ParseExact(formattedDate,"dd.MM.yyyy"),
				ScheduleId = scheduleId,
				Employees = employeesList
			};

			return View(request);
		}

		[HttpPost]
		public async Task<IActionResult> Create(UpsertAssignmentRequest request)
		{
			int scheduleId = request.ScheduleId;

			if (!ModelState.IsValid) {

				request.Employees = await GetEmployeesSelectListAsync(scheduleId);
				return View(request);
			}

			var assignmentDtos = request.Shifts?.Select(s => new AssignmentDto
			{
				EmployeeId = s.EmployeeId,
				StartTime = s.StartTime,
				EndTime = s.EndTime,
				DisplayName = string.Empty
			}).ToList() ?? new List<AssignmentDto>();

			var command = new CreateShiftCommand(
				request.Title!,
				request.Description,
				request.Date,
				assignmentDtos,
				scheduleId);

			var result = await _commandDispatcher.Dispatch<CreateShiftCommand,Result>(command);

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

			var request = new UpsertAssignmentRequest
			{
				Id = shiftTile.Id,
				Title = shiftTile.Title,
				Description = shiftTile.Description,
				Shifts = shiftTile.Shifts.Select(st => new AssignmentRequest
				{
					EmployeeId = st.EmployeeId,
					StartTime = st.StartTime,
					EndTime = st.EndTime
				}).ToList(),
				Employees = await GetEmployeesSelectListAsync(scheduleId),
				ScheduleId = scheduleId
			};

			return View(request);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int scheduleId,UpsertAssignmentRequest request)
		{
			if (!ModelState.IsValid) {

				request.Employees = await GetEmployeesSelectListAsync(scheduleId);
				return View(request);
			}

			var assignmentDtos = request.Shifts?.Select(s => new AssignmentDto
			{
				EmployeeId = s.EmployeeId,
				StartTime = s.StartTime,
				EndTime = s.EndTime,
				DisplayName = string.Empty
			}).ToList() ?? new List<AssignmentDto>();

			var command = new UpdateShiftCommand(
				request.Id,
				request.Title,
				request.Description,
				assignmentDtos,
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

		private async Task<SelectList> GetEmployeesSelectListAsync(int scheduleId)
		{
			var query = new GetFromScheduleQuery(scheduleId);
			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query);

			var employeesItems = employees.Select(employee => new SelectListItem
			{
				Text = $"{employee.Name} {employee.LastName}",
				Value = employee.Id.ToString()
			}).ToList();

			return new SelectList(employeesItems,"Value","Text");
		}
	}
}
