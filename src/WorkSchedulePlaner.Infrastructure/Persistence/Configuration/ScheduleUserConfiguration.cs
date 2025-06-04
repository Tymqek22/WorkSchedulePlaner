using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Identity.Models;

namespace WorkSchedulePlaner.Infrastructure.Persistence.Configuration
{
	public class ScheduleUserConfiguration : IEntityTypeConfiguration<ScheduleUser>
	{
		public void Configure(EntityTypeBuilder<ScheduleUser> builder)
		{
			builder.HasKey(su => new { su.ScheduleId,su.UserId });

			builder
				.HasOne(su => su.Schedule)
				.WithMany(s => s.UsersInSchedule)
				.HasForeignKey(su => su.ScheduleId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne<ApplicationUser>()
				.WithMany(u => u.SchedulesAssigned)
				.HasForeignKey(su => su.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.ToTable("SchedulesUsers");
		}
	}
}
