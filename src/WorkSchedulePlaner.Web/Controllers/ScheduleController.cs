using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleById;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ScheduleController : Controller
	{
		private readonly IWorkScheduleRepository _repository;
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;

		public ScheduleController(
			IWorkScheduleRepository repository, 
			ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher)
		{
			_repository = repository;
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		public async Task<IActionResult> Details(int id)
		{
			//temporary
			var schedule = await _repository.GetWithIncludesAsync(id);
			var query = new GetFromScheduleQuery(schedule.Id);
			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query);

			DateTime startDay = DateTime.Today.AddDays(
				(int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
				(int)DateTime.Today.DayOfWeek);

			var dates = Enumerable
				.Range(0,7)
				.Select(i => startDay.AddDays(i))
				.ToList();

			var viewModel = new ScheduleDetailsVM
			{
				Schedule = new WorkScheduleDto
				{
					Id = schedule.Id,
					Title = schedule.Title,
					ShiftTiles = schedule.ShiftTiles
						.Select(st => new ShiftTileDto
						{
							Id = st.Id,
							Title = st.Title,
							Description = st.Description,
							Date = st.Date,
							Shifts = st.EmployeeShifts
								.Select(es =>
								{
									var employee = employees.FirstOrDefault(e => e.Id == es.EmployeeId);

									return new EmployeeWorkHoursDto
									{
										Employee = employee,
										StartTime = es.StartTime,
										EndTime = es.EndTime
									};
								})
								.ToList()
						})
						.ToList()
				},
				Dates = dates
			};

			return View(viewModel);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(WorkSchedule schedule)
		{
			var command = new CreateScheduleCommand(schedule.Title);

			var result = await _commandDispatcher.Dispatch<CreateScheduleCommand,CreateScheduleResult>(command);

			if (result != CreateScheduleResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot create schedule."
				};

				return View("Error",error);
			}

			return RedirectToAction("Index","Home");
		}

		public async Task<IActionResult> Delete(int scheduleId)
		{
			var command = new DeleteScheduleCommand(scheduleId);

			var result = await _commandDispatcher.Dispatch<DeleteScheduleCommand,DeleteScheduleResult>(command);

			if (result != DeleteScheduleResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot delete schedule."
				};

				return View("Error",error);
			}

			return RedirectToAction("Index","Home");
		}

		public async Task<IActionResult> Update(int scheduleId)
		{
			var query = new GetScheduleByIdQuery(scheduleId);

			var result = await _queryDispatcher.Dispatch<GetScheduleByIdQuery,WorkScheduleDto>(query);

			return View(result);
		}

		[HttpPost]
		public async Task<IActionResult> Update(WorkSchedule schedule)
		{
			var command = new UpdateScheduleCommand(schedule.Id,schedule.Title);

			var result = await _commandDispatcher.Dispatch<UpdateScheduleCommand,UpdateScheduleResult>(command);

			if (result != UpdateScheduleResult.Success) {

				var error = new ErrorViewModel
				{
					RequestId = "Cannot update schedule."
				};

				return View("Error",error);
			}

			return RedirectToAction("Index","Home");
		}
	}
}
