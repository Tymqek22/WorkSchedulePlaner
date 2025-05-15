using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule
{
	public record GetFromScheduleQuery(int ScheduleId) 
		: IQuery<List<Employee>>;
}
