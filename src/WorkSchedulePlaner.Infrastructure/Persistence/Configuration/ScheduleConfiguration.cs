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
				.HasMany(s => s.Employees)
				.WithOne()
				.HasForeignKey(e => e.ScheduleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasMany(s => s.ShiftTiles)
				.WithOne()
				.HasForeignKey(st => st.ScheduleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne<ApplicationUser>()
				.WithMany()
				.HasForeignKey(s => s.OwnerId);
		}
	}
}
