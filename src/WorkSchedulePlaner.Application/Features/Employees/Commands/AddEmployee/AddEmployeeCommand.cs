using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee
{
	public record AddEmployeeCommand(
		string Name,
		string LastName,
		string? Position,
		string? UserEmail,
		int ScheduleId)
		: ICommand<Result>;
}
