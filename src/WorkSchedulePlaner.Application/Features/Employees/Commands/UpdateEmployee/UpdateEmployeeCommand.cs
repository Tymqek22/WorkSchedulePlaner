using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee
{
	public record UpdateEmployeeCommand(
		int Id,
		string FirstName,
		string LastName,
		string? Position,
		string? Email,
		int ScheduleId)
		: ICommand<Result>;
}
