using Microsoft.Extensions.DependencyInjection;
using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddHandlers();

			return services;
		}

		private static IServiceCollection AddHandlers(this IServiceCollection services) 
		{
			services.Scan(scan => scan
				.FromAssemblies(typeof(DependencyInjection).Assembly)
				.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
					.AsImplementedInterfaces()
					.WithScopedLifetime()
				.AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
					.AsImplementedInterfaces()
					.WithScopedLifetime());

			return services;
		}
	}
}
