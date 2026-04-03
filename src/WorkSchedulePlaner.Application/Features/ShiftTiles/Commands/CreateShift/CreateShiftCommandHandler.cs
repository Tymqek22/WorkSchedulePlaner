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

			if (command.EmployeeAssignments is null)
				return Result.Failure(Errors.ShiftTile.NoEmployeesAssigned);

			var domainAssignments = command.EmployeeAssignments
				.Select(a => new ShiftAssignment(a.EmployeeId,new TimeRange(a.StartTime,a.EndTime)))
				.ToList();

			var result = schedule.CreateShift(command.Title,command.Description,command.Date,domainAssignments);

			if (!result.IsSuccess)
				return result;
			
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
