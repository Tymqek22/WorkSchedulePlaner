using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Abstractions.Services;
using WorkSchedulePlaner.Application.Common;
using WorkSchedulePlaner.Domain.Repositories;
using WorkSchedulePlaner.Infrastructure.Dispatching;
using WorkSchedulePlaner.Infrastructure.Identity.Authorization;
using WorkSchedulePlaner.Infrastructure.Identity.Models;
using WorkSchedulePlaner.Infrastructure.Identity.Services;
using WorkSchedulePlaner.Infrastructure.Persistence;
using WorkSchedulePlaner.Infrastructure.Repository;

namespace WorkSchedulePlaner.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDatabase(configuration);
			services.AddRepositories();
			services.AddIdentity();
			services.AddAuthorization();
			services.AddDispatchers();

			return services;
		}

		private static IServiceCollection AddDatabase(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("SchedulePlaner")));

			return services;
		}

		private static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IWorkScheduleRepository,WorkScheduleRepository>();
			services.AddScoped<IShiftTileRepository,ShiftTileRepository>();
			services.AddScoped<IEmployeeRepository,EmployeeRepository>();
			services.AddScoped<IUnitOfWork,UnitOfWork>();

			return services;
		}

		private static IServiceCollection AddIdentity(this IServiceCollection services)
		{
			services.AddDefaultIdentity<ApplicationUser>(options =>
			options.SignIn.RequireConfirmedAccount = false)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultUI()
				.AddDefaultTokenProviders();

			services.AddScoped<IIdentityService,IdentityService>();

			return services;
		}

		private static IServiceCollection AddAuthorization(this IServiceCollection services)
		{
			services.AddAuthorization(options =>
			{
				options.AddPolicy("ScheduleAdminPolicy",policy =>
					policy.Requirements.Add(new CanManageScheduleRequirement()));
			});

			services.AddScoped<IAuthorizationHandler,ScheduleAuthorizationHandler>();

			return services;
		}

		private static IServiceCollection AddDispatchers(this IServiceCollection services)
		{
			services.AddScoped<ICommandDispatcher,CommandDispatcher>();
			services.AddScoped<IQueryDispatcher,QueryDispatcher>();

			return services;
		}
	}
}
