using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Identity.Models;

namespace WorkSchedulePlaner.Infrastructure.Persistence.Configuration
{
	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.HasKey(e => e.Id);

			builder
				.HasOne(e => e.Schedule)
				.WithMany(s => s.Employees)
				.HasForeignKey(e => e.ScheduleId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne<ApplicationUser>()
				.WithMany(u => u.AssignedEmployees)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
