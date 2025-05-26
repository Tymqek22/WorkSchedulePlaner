using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Mappings;

namespace WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleById
{
	public class GetScheduleByIdQueryHandler : IQueryHandler<GetScheduleByIdQuery,WorkScheduleDto>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;

		public GetScheduleByIdQueryHandler(IWorkScheduleRepository workScheduleRepository)
		{
			_workScheduleRepository = workScheduleRepository;
		}

		public async Task<WorkScheduleDto> Handle(
			GetScheduleByIdQuery query,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _workScheduleRepository.GetByIdAsync(query.Id);

			return schedule.MapToDto();
		}
	}
}
