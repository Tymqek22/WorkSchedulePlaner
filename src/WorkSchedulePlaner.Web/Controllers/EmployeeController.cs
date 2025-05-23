using Microsoft.AspNetCore.Mvc;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.DeleteEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee;
using WorkSchedulePlaner.Application.Features.Employees.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;

namespace WorkSchedulePlaner.Web.Controllers
{
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
			var query = new GetFromScheduleQuery(scheduleId);

			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query);

			return View(employees);
		}

		public IActionResult Create(int scheduleId)
		{
			ViewBag.ScheduleId = scheduleId;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Employee employee)
		{
			var command = new AddEmployeeCommand(
				employee.Name,
				employee.LastName,
				employee.Position,
				employee.ScheduleId);

			var result = await _commandDispatcher.Dispatch<AddEmployeeCommand,AddEmployeeResult>(command);

			if (result != AddEmployeeResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add employee."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = command.ScheduleId });
		}

		public async Task<IActionResult> Update(int scheduleId, int employeeId)
		{
			var query = new GetByIdFromScheduleQuery(scheduleId,employeeId);

			var employee = await _queryDispatcher.Dispatch<GetByIdFromScheduleQuery,EmployeeDto>(query);

			return View(employee);
		}

		[HttpPost]
		public async Task<IActionResult> Update(Employee employee)
		{
			var command = new UpdateEmployeeCommand(
				employee.Id,
				employee.Name,
				employee.LastName,
				employee.Position);

			var result = await _commandDispatcher.Dispatch<UpdateEmployeeCommand,UpdateEmployeeResult>(command);

			if (result != UpdateEmployeeResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot update employee."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = employee.ScheduleId });
		}

		public async Task<IActionResult> Delete(int scheduleId, int employeeId)
		{
			var command = new DeleteEmployeeCommand(employeeId);

			var result = await _commandDispatcher.Dispatch<DeleteEmployeeCommand,DeleteEmployeeResult>(command);

			if (result != DeleteEmployeeResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot delete employee."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = scheduleId });
		}
	}
}
