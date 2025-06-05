using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetUserRoleInSchedule
{
	public class GetUserRoleInScheduleQueryHandler : IQueryHandler<GetUserRoleInScheduleQuery,string>
	{
		private readonly IScheduleUserRepository _scheduleUserRepository;

		public GetUserRoleInScheduleQueryHandler(IScheduleUserRepository scheduleUserRepository)
		{
			_scheduleUserRepository = scheduleUserRepository;
		}

		public async Task<string> Handle(
			GetUserRoleInScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			return await _scheduleUserRepository.GetUserRoleInSchedule(query.UserId,query.ScheduleId);
		}
	}
}
