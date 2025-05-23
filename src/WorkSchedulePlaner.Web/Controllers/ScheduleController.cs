using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Web.Models;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Controllers
{
	public class ScheduleController : Controller
	{
		private readonly IWorkScheduleRepository _repository;
		private readonly ICommandDispatcher _commandDispatcher;

		public ScheduleController(IWorkScheduleRepository repository, ICommandDispatcher commandDispatcher)
		{
			_repository = repository;
			_commandDispatcher = commandDispatcher;
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
			//temporary
			var schedule = await _repository.GetByIdAsync(scheduleId);

			return View(schedule);
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
