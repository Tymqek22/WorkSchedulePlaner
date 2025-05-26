using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift
{
	public class UpdateShiftCommandHandler : ICommandHandler<UpdateShiftCommand,UpdateShiftResult>
	{
		private readonly IShiftTileRepository _shiftTileRepository;
		private readonly IRepository<EmployeeShift> _shiftsRepository;
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateShiftCommandHandler(
			IShiftTileRepository shiftTileRepository,
			IRepository<EmployeeShift> shiftsRepository,
			IRepository<Employee> employeeRepository,
			IUnitOfWork unitOfWork)
		{
			_shiftTileRepository = shiftTileRepository;
			_shiftsRepository = shiftsRepository;
			_employeeRepository = employeeRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<UpdateShiftResult> Handle(
			UpdateShiftCommand command,
			CancellationToken cancellationToken = default)
		{
			//find tile in db
			var tile = await _shiftTileRepository.GetByIdAsync(command.ShiftTileId);

			if (tile is null)
				return UpdateShiftResult.Failure;

			if (!this.IsAnyEmployeeAssigned(command.EmployeeWorkHours))
				return UpdateShiftResult.Failure;
			else if (!await this.AllEmployeesExist(command.EmployeeWorkHours))
				return UpdateShiftResult.Failure;
			else if (this.IsEmployeeDuplicatedOnShift(command.EmployeeWorkHours))
				return UpdateShiftResult.Failure;

			foreach (var employeeShift in command.EmployeeWorkHours) {

				if (!await this.IsEmployeeAssignedOnce(employeeShift.Employee.Id,tile.Date))
					return UpdateShiftResult.Failure;
			}

			//delete employee shifts for that tile
			await _shiftsRepository.DeleteManyAsync(es => es.ShiftTileId == command.ShiftTileId);

			//add updated shifts for tile
			foreach (var employeeShift in command.EmployeeWorkHours) {

				var shift = new EmployeeShift
				{
					EmployeeId = employeeShift.Employee.Id,
					ShiftTileId = command.ShiftTileId,
					StartTime = employeeShift.StartTime,
					EndTime = employeeShift.EndTime
				};

				await _shiftsRepository.InsertAsync(shift);
			}

			//update rest of tile
			tile.Title = command.Title;
			tile.Description = command.Description;

			await _unitOfWork.SaveAsync();

			return UpdateShiftResult.Success;
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

		private async Task<bool> IsEmployeeAssignedOnce(int employeeId,DateTime date)
		{
			var shiftTilesFromToday = await _shiftTileRepository.GetShiftTilesFromPeriod(date,date);

			if (shiftTilesFromToday.Any(st => st.EmployeeShifts.Any(es => es.EmployeeId == employeeId)))
				return false;

			return true;
		}

		private bool IsAnyEmployeeAssigned(List<EmployeeWorkHoursDto> employeeShifts)
		{
			return employeeShifts is not null;
		}

		private bool IsEmployeeDuplicatedOnShift(List<EmployeeWorkHoursDto> employeeShifts)
		{
			return employeeShifts
				.GroupBy(es => es.Employee)
				.Select(e => e.Count())
				.Any(c => c > 1);
		}
	}
}
