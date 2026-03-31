using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.DeleteEmployee
{
	public record DeleteEmployeeCommand(int EmployeeId,int ScheduleId) 
		: ICommand<Result>;
}
