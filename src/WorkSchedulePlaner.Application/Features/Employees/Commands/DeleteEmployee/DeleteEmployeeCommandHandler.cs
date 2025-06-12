using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Errors;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.DeleteEmployee
{
	public class DeleteEmployeeCommandHandler
		: ICommandHandler<DeleteEmployeeCommand,Result>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IRepository<EmployeeShift> _shiftsRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteEmployeeCommandHandler(
			IRepository<Employee> employeeRepository,
			IRepository<EmployeeShift> shiftsRepository,
			IUnitOfWork unitOfWork)
		{
			_employeeRepository = employeeRepository;
			_shiftsRepository = shiftsRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			DeleteEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			//find employee in DB
			var employee = await _employeeRepository.GetByIdAsync(command.Id);

			if (employee is null)
				return Result.Failure(Errors.Employee.NotFound);

			//delete all employees shifts
			await _shiftsRepository.DeleteManyAsync(es => es.EmployeeId == employee.Id);

			//delete employee from DB
			await _employeeRepository.DeleteAsync(employee.Id);
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
