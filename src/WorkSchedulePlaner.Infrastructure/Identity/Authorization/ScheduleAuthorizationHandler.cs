using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetUserRoleInSchedule;
using WorkSchedulePlaner.Infrastructure.Common.Authorization;

namespace WorkSchedulePlaner.Infrastructure.Identity.Authorization
{
	public class ScheduleAuthorizationHandler : AuthorizationHandler<CanManageScheduleRequirement,int>
	{
		private readonly IQueryDispatcher _queryDispatcher;

		public ScheduleAuthorizationHandler(IQueryDispatcher queryDispatcher)
		{
			_queryDispatcher = queryDispatcher;
		}

		protected async override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			CanManageScheduleRequirement requirement,
			int scheduleId)
		{
			var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId)) return;

			var query = new GetUserRoleInScheduleQuery(userId,scheduleId);

			var role = await _queryDispatcher.Dispatch<GetUserRoleInScheduleQuery,string>(query);

			if (role == "admin")
				context.Succeed(requirement);
		}
	}
}
