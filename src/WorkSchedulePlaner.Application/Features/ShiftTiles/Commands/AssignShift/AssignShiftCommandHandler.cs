using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift
{
	public class AssignShiftCommandHandler : ICommandHandler<AssignShiftCommand,AssignShiftResult>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IRepository<EmployeeShift> _employeeShiftRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AssignShiftCommandHandler(
			IRepository<Employee> employeeRepository,
			IRepository<ShiftTile> shiftTileRepository,
			IRepository<EmployeeShift> employeeShiftRepository,
			IUnitOfWork unitOfWork)
		{
			_employeeRepository = employeeRepository;
			_shiftTileRepository = shiftTileRepository;
			_employeeShiftRepository = employeeShiftRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<AssignShiftResult> Handle(
			AssignShiftCommand command,
			CancellationToken cancellationToken = default)
		{
			if (!await this.AllEmployeesExist(command.EmployeeWorkHours))
				return AssignShiftResult.UserNotFound;

			//add tile to DB
			var newShiftTile = new ShiftTile
			{
				Title = command.Title,
				Description = command.Description,
				Date = command.Date,
				ScheduleId = command.ScheduleId
			};
			await _shiftTileRepository.InsertAsync(newShiftTile);

			//add EmployeeShift to DB
			foreach (var employeeShift in command.EmployeeWorkHours) {

				var newEmployeeShift = new EmployeeShift
				{
					EmployeeId = employeeShift.Employee.Id,
					ShiftTile = newShiftTile,
					StartTime = employeeShift.StartTime,
					EndTime = employeeShift.EndTime
				};
				await _employeeShiftRepository.InsertAsync(newEmployeeShift);
			}
			await _unitOfWork.SaveAsync();

			return AssignShiftResult.Success;
		}

		private async Task<bool> AllEmployeesExist(List<EmployeeWorkHoursDto> employeeWorkHours)
		{
			foreach (var employee in employeeWorkHours) {

				var exists = await _employeeRepository.GetByIdAsync(employee.Employee.Id);

				if (exists is null)
					return false;
			}

			return true;
		}
	}
}
