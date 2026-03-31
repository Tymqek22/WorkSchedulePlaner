using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee
{
	public record AddEmployeeCommand(
		string FirstName,
		string LastName,
		string? Position,
		string? UserEmail,
		int ScheduleId)
		: ICommand<Result>;
}
