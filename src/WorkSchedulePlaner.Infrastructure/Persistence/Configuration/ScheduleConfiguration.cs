using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Identity.Models;

namespace WorkSchedulePlaner.Infrastructure.Persistence.Configuration
{
	public class ScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
	{
		public void Configure(EntityTypeBuilder<WorkSchedule> builder)
		{
			builder.HasKey(s => s.Id);

			builder.Property(s => s.OwnerId).IsRequired();

			builder
				.HasOne<ApplicationUser>()
				.WithMany(u => u.Schedules)
				.HasForeignKey(s => s.OwnerId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
