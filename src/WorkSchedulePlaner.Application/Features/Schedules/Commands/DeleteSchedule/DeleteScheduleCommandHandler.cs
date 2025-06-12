using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Errors;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule
{
	public class DeleteScheduleCommandHandler
		: ICommandHandler<DeleteScheduleCommand,Result>
	{
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IRepository<EmployeeShift> _employeeShiftRepository;
		private readonly IRepository<ScheduleUser> _scheduleUserRepository;
		private readonly IWorkScheduleRepository _scheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteScheduleCommandHandler(
			IRepository<ShiftTile> shiftTileRepository,
			IRepository<Employee> employeeRepository,
			IRepository<EmployeeShift> employeeShiftRepository,
			IRepository<ScheduleUser> scheduleUserRepository,
			IWorkScheduleRepository scheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_shiftTileRepository = shiftTileRepository;
			_employeeRepository = employeeRepository;
			_employeeShiftRepository = employeeShiftRepository;
			_scheduleUserRepository = scheduleUserRepository;
			_scheduleRepository = scheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			DeleteScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			//find schedule in DB
			var schedule = await _scheduleRepository.GetByIdAsync(command.Id);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			//get shift tiles to delete
			var shiftTiles = await _shiftTileRepository.GetAsync(st => st.ScheduleId == schedule.Id);
			var shiftTileIds = shiftTiles.Select(st => st.Id).ToList();

			//delete all shifts
			await _employeeShiftRepository.DeleteManyAsync(es => shiftTileIds.Contains(es.ShiftTileId));

			//delete all tiles
			await _shiftTileRepository.DeleteManyAsync(st => st.ScheduleId == schedule.Id);

			//delete all employees
			await _employeeRepository.DeleteManyAsync(e => e.ScheduleId == schedule.Id);

			//delete all userRoles in schedule
			await _scheduleUserRepository.DeleteManyAsync(su => su.ScheduleId == schedule.Id);

			//delete schedule
			await _scheduleRepository.DeleteAsync(schedule.Id);
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
