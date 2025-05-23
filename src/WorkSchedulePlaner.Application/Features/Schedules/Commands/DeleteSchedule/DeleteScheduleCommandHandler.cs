using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule
{
	public class DeleteScheduleCommandHandler
		: ICommandHandler<DeleteScheduleCommand,DeleteScheduleResult>
	{
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IRepository<EmployeeShift> _employeeShiftRepository;
		private readonly IWorkScheduleRepository _scheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteScheduleCommandHandler(
			IRepository<ShiftTile> shiftTileRepository,
			IRepository<Employee> employeeRepository,
			IRepository<EmployeeShift> employeeShiftRepository,
			IWorkScheduleRepository scheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_shiftTileRepository = shiftTileRepository;
			_employeeRepository = employeeRepository;
			_employeeShiftRepository = employeeShiftRepository;
			_scheduleRepository = scheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<DeleteScheduleResult> Handle(
			DeleteScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			//find schedule in DB
			var schedule = await _scheduleRepository.GetByIdAsync(command.Id);

			if (schedule is null)
				return DeleteScheduleResult.Failure;

			//get shift tiles to delete
			var shiftTiles = await _shiftTileRepository.GetAsync(st => st.ScheduleId == schedule.Id);
			var shiftTileIds = shiftTiles.Select(st => st.Id).ToList();

			//delete all shifts
			await _employeeShiftRepository.DeleteManyAsync(es => shiftTileIds.Contains(es.ShiftTileId));

			//delete all tiles
			await _shiftTileRepository.DeleteManyAsync(st => st.ScheduleId == schedule.Id);

			//delete all employees
			await _employeeRepository.DeleteManyAsync(e => e.ScheduleId == schedule.Id);

			//delete schedule
			await _scheduleRepository.DeleteAsync(schedule.Id);
			await _unitOfWork.SaveAsync();

			return DeleteScheduleResult.Success;
		}
	}
}
