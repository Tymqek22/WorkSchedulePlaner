using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Application.Schedules.AddEmployee;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ScheduleController : Controller
	{
		private readonly IWorkScheduleRepository _repository;
		private readonly IRepository<Employee> _employeeRepository;
		private readonly AddEmployee _addEmployee;

		public ScheduleController(IWorkScheduleRepository repository, IRepository<Employee> employeeRepository,
			AddEmployee addEmployee)
		{
			_repository = repository;
			_employeeRepository = employeeRepository;
			_addEmployee = addEmployee;
		}

		public async Task<IActionResult> Details(int id)
		{
			//temporary
			var schedule = await _repository.GetWithIncludesAsync(id);

			DateTime startDay = DateTime.Today.AddDays(
				(int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
				(int)DateTime.Today.DayOfWeek);

			var dates = Enumerable
				.Range(0,7)
				.Select(i => startDay.AddDays(i))
				.ToList();

			var viewModel = new ScheduleDetailsVM
			{
				Schedule = schedule,
				Dates = dates
			};

			return View(viewModel);
		}

		public async Task<IActionResult> Employees(int id)
		{
			//temporary
			var employees = await _employeeRepository.GetAsync(e => e.ScheduleId == id);

			return View(employees.ToList());
		}

		public IActionResult AddEmployee(int id)
		{
			ViewBag.ScheduleId = id;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddEmployee(Employee employee)
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

		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Delete()
		{
			return View();
		}

		public IActionResult Update()
		{
			return View();
		}
	}
}
