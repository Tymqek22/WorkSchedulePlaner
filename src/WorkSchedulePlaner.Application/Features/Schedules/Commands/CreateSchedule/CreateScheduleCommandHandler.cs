using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule
{
	public class CreateScheduleCommandHandler
		: ICommandHandler<CreateScheduleCommand,Result>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateScheduleCommandHandler(
			IWorkScheduleRepository workScheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_workScheduleRepository = workScheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			CreateScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			var newSchedule = new WorkSchedule(command.Title,command.OwnerId);

			await _workScheduleRepository.InsertAsync(newSchedule);
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
