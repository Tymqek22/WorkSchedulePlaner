using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Mappings;

namespace WorkSchedulePlaner.Application.Features.Schedules.Queries.GetUserSchedules
{
	public class GetUserSchedulesQueryHandler : IQueryHandler<GetUserSchedulesQuery,List<WorkScheduleDto>>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;

		public GetUserSchedulesQueryHandler(IWorkScheduleRepository workScheduleRepository)
		{
			_workScheduleRepository = workScheduleRepository;
		}

		public async Task<List<WorkScheduleDto>> Handle(
			GetUserSchedulesQuery query,
			CancellationToken cancellationToken = default)
		{
			var userSchedules = await _workScheduleRepository.GetAllUserSchedules(query.UserId);

			return userSchedules
				.Select(ws =>
				{
					var dto = ws.MapToDto();

					if (ws.OwnerId == query.UserId)
						dto.IsAdmin = true;
					else
						dto.IsAdmin = false;

					return dto;
				})
				.ToList();
		}
	}
}
