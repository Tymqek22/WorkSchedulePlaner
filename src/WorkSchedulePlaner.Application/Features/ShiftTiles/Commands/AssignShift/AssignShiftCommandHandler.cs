using System.Diagnostics.CodeAnalysis;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift
{
	public class AssignShiftCommandHandler : ICommandHandler<AssignShiftCommand,AssignShiftResult>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IShiftTileRepository _shiftTileRepository;
		private readonly IRepository<EmployeeShift> _employeeShiftRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AssignShiftCommandHandler(
			IRepository<Employee> employeeRepository,
			IShiftTileRepository shiftTileRepository,
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
			//temp validation
			if (!this.IsAnyEmployeeAssigned(command.EmployeeWorkHours))
				return AssignShiftResult.Failure;
			else if (!await this.AllEmployeesExist(command.EmployeeWorkHours))
				return AssignShiftResult.UserNotFound;
			else if (this.IsEmployeeDuplicatedOnShift(command.EmployeeWorkHours))
				return AssignShiftResult.Failure;

			foreach (var employeeShift in command.EmployeeWorkHours) {

				if (!await this.IsEmployeeAssignedOnce(employeeShift.Employee.Id,command.Date))
					return AssignShiftResult.Failure;
			}

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
