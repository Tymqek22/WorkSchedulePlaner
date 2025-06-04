using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleById;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleDetailsFromPeriod;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	[Authorize]
	public class ScheduleController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;

		public ScheduleController( 
			ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		public async Task<IActionResult> Details(int id, int weekOffset = 0)
		{
			DateTime startDay = DateTime.Today.AddDays(
				(int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
				(int)DateTime.Today.DayOfWeek)
				.AddDays(7 * weekOffset);

			ViewBag.WeekOffset = weekOffset;

			var dates = Enumerable
				.Range(0,7)
				.Select(i => startDay.AddDays(i))
				.ToList();

			var query1 = new GetScheduleDetailsFromPeriodQuery(id,dates[0],dates[6]);
			var schedule = await _queryDispatcher.Dispatch<GetScheduleDetailsFromPeriodQuery,WorkScheduleDto>(query1);

			var query2 = new GetFromScheduleQuery(schedule.Id);
			var employees = await _queryDispatcher.Dispatch<GetFromScheduleQuery,List<EmployeeDto>>(query2);

			var viewModel = new ScheduleDetailsVM
			{
				Schedule = schedule,
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
