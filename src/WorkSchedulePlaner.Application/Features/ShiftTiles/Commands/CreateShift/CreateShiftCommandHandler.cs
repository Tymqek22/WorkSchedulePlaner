using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Repositories;
using WorkSchedulePlaner.Domain.ValueObjects;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift
{
	public class AssignShiftCommandHandler : ICommandHandler<CreateShiftCommand,Result>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AssignShiftCommandHandler(
			IWorkScheduleRepository workScheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_workScheduleRepository = workScheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			CreateShiftCommand command,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _workScheduleRepository.GetByIdWithDetailsAsync(command.ScheduleId);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			var domainAssignments = command.EmployeeWorkHours
				.Select(wh => new ShiftAssignment(wh.EmployeeId,new TimeRange(wh.StartTime,wh.EndTime)))
				.ToList();

			var result = await schedule.CreateShift(command.Title,command.Description,domainAssignments);

			if (!result.IsSuccess)
				return result;
			
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
