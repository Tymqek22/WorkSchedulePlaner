using Microsoft.AspNetCore.Mvc;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Application.Schedules.AddEmployee;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly AddEmployee _addEmployee;

		public EmployeeController(IRepository<Employee> employeeRepository, AddEmployee addEmployee)
		{
			_employeeRepository = employeeRepository;
			_addEmployee = addEmployee;
		}

		public async Task<IActionResult> Employees(int scheduleId)
		{
			//temporary
			var employees = await _employeeRepository.GetAsync(e => e.ScheduleId == scheduleId);

			return View(employees.ToList());
		}

		public IActionResult Create(int scheduleId)
		{
			ViewBag.ScheduleId = scheduleId;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Employee employee)
		{
			var request = new AddEmployeeRequest(
				employee.Name,
				employee.LastName,
				employee.Position,
				employee.ScheduleId);

			var result = await _addEmployee.Handle(request);

			if (result != AddEmployeeResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot add employee."
				};

				return View("Error",error);
			}

			return RedirectToAction("Details","Schedule",new { id = request.ScheduleId });
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
