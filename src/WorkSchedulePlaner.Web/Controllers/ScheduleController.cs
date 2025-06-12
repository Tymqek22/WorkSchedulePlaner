using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetUserRoleInSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleById;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleDetailsFromPeriod;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetUserSchedules;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Identity.Models;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	[Authorize]
	public class ScheduleController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly UserManager<ApplicationUser> _userManager;

		public ScheduleController( 
			ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher,
			UserManager<ApplicationUser> userManager)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var userId = _userManager.GetUserId(User);

			var query1 = new GetUserSchedulesQuery(userId);

			var schedules = await _queryDispatcher.Dispatch<GetUserSchedulesQuery,List<WorkScheduleDto>>(query1);

			return View(schedules);
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

			var userId = _userManager.GetUserId(User);

			var query3 = new GetUserRoleInScheduleQuery(userId,id);
			var result = await _queryDispatcher.Dispatch<GetUserRoleInScheduleQuery,string>(query3);
			bool admin;
			if (result == "admin")
				admin = true;
			else
				admin = false;

			var viewModel = new ScheduleDetailsVM
			{
				Schedule = schedule,
				Dates = dates,
				IsCurrentUserAdmin = admin
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
			var command = new CreateScheduleCommand(schedule.Title,schedule.OwnerId);

			var result = await _commandDispatcher.Dispatch<CreateScheduleCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Index","Schedule");
		}

		public async Task<IActionResult> Delete(int scheduleId)
		{
			var command = new DeleteScheduleCommand(scheduleId);

			var result = await _commandDispatcher.Dispatch<DeleteScheduleCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Index","Schedule");
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

			var result = await _commandDispatcher.Dispatch<UpdateScheduleCommand,Result>(command);

			if (result.IsFailure) {

				var error = new ErrorViewModel
				{
					RequestId = result.Error.Message
				};

				return View("Error",error);
			}

			return RedirectToAction("Index","Schedule");
		}
	}
}
