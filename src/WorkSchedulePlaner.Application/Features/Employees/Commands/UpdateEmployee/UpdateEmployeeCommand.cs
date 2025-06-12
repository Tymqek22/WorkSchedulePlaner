using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee
{
	public record UpdateEmployeeCommand(
		int Id,
		string Name,
		string LastName,
		string? Position,
		string? Email)
		: ICommand<Result>;
}
