using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetUserRoleInSchedule
{
	public class GetUserRoleInScheduleQueryHandler : IQueryHandler<GetUserRoleInScheduleQuery,string>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;

		public GetUserRoleInScheduleQueryHandler(IWorkScheduleRepository workScheduleRepository)
		{
			_workScheduleRepository = workScheduleRepository;
		}

		public async Task<string> Handle(
			GetUserRoleInScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _workScheduleRepository.GetByIdAsync(query.ScheduleId);

			if (schedule.OwnerId == query.UserId)
				return "admin";

			return "employee";
		}
	}
}
