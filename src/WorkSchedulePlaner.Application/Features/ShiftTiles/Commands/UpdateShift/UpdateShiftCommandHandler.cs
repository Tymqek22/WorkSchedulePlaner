using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Repositories;
using WorkSchedulePlaner.Domain.ValueObjects;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift
{
	public class UpdateShiftCommandHandler : ICommandHandler<UpdateShiftCommand,Result>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateShiftCommandHandler(
			IWorkScheduleRepository workScheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_workScheduleRepository = workScheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			UpdateShiftCommand command,
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

			var result = schedule.UpdateShift(
				command.ShiftTileId,
				command.Title,
				command.Description,
				domainAssignments);

			if (!result.IsSuccess)
				return result;

			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
