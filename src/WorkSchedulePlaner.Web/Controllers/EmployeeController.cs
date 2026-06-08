using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.DeleteEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.Requests;

namespace WorkSchedulePlaner.Web.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;

		public EmployeeController(ICommandDispatcher commandDispatcher,IQueryDispatcher queryDispatcher)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		public async Task<IActionResult> Employees(int scheduleId)
		{
			ViewBag.ScheduleId = scheduleId;
			var query = new GetFromScheduleQuery(scheduleId);

			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query);

			return View(employees);
		}

		public IActionResult Create(int scheduleId)
		{
			var request = new UpsertEmployeeRequest
			{
				ScheduleId = scheduleId
			};

			return View(request);
		}

		[HttpPost]
		public async Task<IActionResult> Create(UpsertEmployeeRequest employee)
		{
			if (!ModelState.IsValid) {
				return View(employee);
			}

			var command = new AddEmployeeCommand(
				employee.Name,
				employee.LastName,
				employee.Position,
				employee.Email,
				employee.ScheduleId);

			var result = await _commandDispatcher.Dispatch<AddEmployeeCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Employees","Employee",new { scheduleId = command.ScheduleId });
		}

		public async Task<IActionResult> Update(int scheduleId, int employeeId)
		{
			var query = new GetByIdFromScheduleQuery(scheduleId,employeeId);

			var employee = await _queryDispatcher.Dispatch<GetByIdFromScheduleQuery,EmployeeDto>(query);

			var request = new UpsertEmployeeRequest
			{
				Id = employee.Id,
				Name = employee.Name,
				LastName = employee.LastName,
				Position = employee.Position,
				Email = employee.Email,
				ScheduleId = scheduleId
			};

			return View(request);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpsertEmployeeRequest employee)
		{
			if (!ModelState.IsValid) {
				return View(employee);
			}

			var command = new UpdateEmployeeCommand(
				employee.Id,
				employee.Name,
				employee.LastName,
				employee.Position,
				employee.Email,
				employee.ScheduleId);

			var result = await _commandDispatcher.Dispatch<UpdateEmployeeCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Employees","Employee",new { scheduleId = employee.ScheduleId });
		}

		public async Task<IActionResult> Delete(int scheduleId, int employeeId)
		{
			var command = new DeleteEmployeeCommand(employeeId,scheduleId);

			var result = await _commandDispatcher.Dispatch<DeleteEmployeeCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Employees","Employee",new { scheduleId = scheduleId });
		}
	}
}
