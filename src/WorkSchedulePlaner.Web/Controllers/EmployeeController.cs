using Microsoft.AspNetCore.Mvc;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class EmployeeController : Controller
	{
		public async Task<IActionResult> Employees(int scheduleId,
			[FromServices] IQueryHandler<GetFromScheduleQuery,List<Employee>> handler)
		{
			var query = new GetFromScheduleQuery(scheduleId);

			var employees = await handler.Handle(query);

			return View(employees);
		}

		public IActionResult Create(int scheduleId)
		{
			ViewBag.ScheduleId = scheduleId;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Employee employee,
			[FromServices] ICommandHandler<AddEmployeeCommand,AddEmployeeResult> handler)
		{
			var command = new AddEmployeeCommand(
				employee.Name,
				employee.LastName,
				employee.Position,
				employee.ScheduleId);

			var result = await handler.Handle(command);

			if (result != AddEmployeeResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add employee."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = command.ScheduleId });
		}

		public async Task<IActionResult> Update(int scheduleId, int employeeId,
			[FromServices] IQueryHandler<GetByIdFromScheduleQuery,Employee> handler)
		{
			var query = new GetByIdFromScheduleQuery(scheduleId,employeeId);

			var employee = await handler.Handle(query);

			return View(employee);
		}

		[HttpPost]
		public async Task<IActionResult> Update(Employee employee,
			[FromServices] ICommandHandler<UpdateEmployeeCommand,UpdateEmployeeResult> handler)
		{
			var command = new UpdateEmployeeCommand(
				employee.Id,
				employee.Name,
				employee.LastName,
				employee.Position);

			var result = await handler.Handle(command);

			if (result != UpdateEmployeeResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot update employee."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = employee.ScheduleId });
		}

		public IActionResult Delete()
		{
			return View();
		}
	}
}
