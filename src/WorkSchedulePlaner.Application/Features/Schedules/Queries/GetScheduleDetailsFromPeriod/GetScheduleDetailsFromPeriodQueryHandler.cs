using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Mappings;

namespace WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleDetailsFromPeriod
{
	public class GetScheduleDetailsFromPeriodQueryHandler
		: IQueryHandler<GetScheduleDetailsFromPeriodQuery,WorkScheduleDto>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;

		public GetScheduleDetailsFromPeriodQueryHandler(IWorkScheduleRepository workScheduleRepository)
		{
			_workScheduleRepository = workScheduleRepository;
		}

		public async Task<WorkScheduleDto> Handle(
			GetScheduleDetailsFromPeriodQuery query,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _workScheduleRepository.GetScheduleDetailsFromPeriod(
				query.Id,
				query.StartDate,
				query.EndDate);

			return schedule.MapToDto();
		}
	}
}
