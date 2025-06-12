using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule
{
	public class CreateScheduleCommandHandler
		: ICommandHandler<CreateScheduleCommand,Result>
	{
		private readonly IWorkScheduleRepository _scheduleRepository;
		private readonly IRepository<ScheduleUser> _scheduleUserRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateScheduleCommandHandler(
			IWorkScheduleRepository scheduleRepository,
			IRepository<ScheduleUser> scheduleUserRepository,
			IUnitOfWork unitOfWork)
		{
			_scheduleRepository = scheduleRepository;
			_scheduleUserRepository = scheduleUserRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			CreateScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			var newSchedule = new WorkSchedule
			{
				Title = command.Title,
				OwnerId = command.OwnerId
			};

			var scheduleUserRole = new ScheduleUser
			{
				Schedule = newSchedule,
				UserId = command.OwnerId,
				Role = "admin"
			};

			await _scheduleRepository.InsertAsync(newSchedule);
			await _scheduleUserRepository.InsertAsync(scheduleUserRole);
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
