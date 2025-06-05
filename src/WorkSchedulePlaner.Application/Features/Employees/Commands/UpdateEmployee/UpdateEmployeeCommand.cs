using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee
{
	public record UpdateEmployeeCommand(
		int Id,
		string Name,
		string LastName,
		string? Position,
		string? Email)
		: ICommand<UpdateEmployeeResult>;
}
