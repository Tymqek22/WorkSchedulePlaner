using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.DeleteEmployee
{
	public class DeleteEmployeeCommandHandler
		: ICommandHandler<DeleteEmployeeCommand,DeleteEmployeeResult>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IEmployeeShiftRepository _shiftsRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteEmployeeCommandHandler(
			IRepository<Employee> employeeRepository,
			IEmployeeShiftRepository shiftsRepository,
			IUnitOfWork unitOfWork)
		{
			_employeeRepository = employeeRepository;
			_shiftsRepository = shiftsRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<DeleteEmployeeResult> Handle(
			DeleteEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			//find employee in DB
			var employee = await _employeeRepository.GetByIdAsync(command.Id);

			if (employee is null)
				return DeleteEmployeeResult.Failure;

			//delete all employees shifts
			await _shiftsRepository.DeleteManyAsync(es => es.EmployeeId == employee.Id);

			//delete employee from DB
			await _employeeRepository.DeleteAsync(employee.Id);
			await _unitOfWork.SaveAsync();

			return DeleteEmployeeResult.Success;
		}
	}
}
