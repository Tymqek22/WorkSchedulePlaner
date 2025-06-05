using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee
{
	public record AddEmployeeCommand(
		string Name,
		string LastName,
		string? Position,
		string? UserEmail,
		int ScheduleId)
		: ICommand<AddEmployeeResult>;
}
