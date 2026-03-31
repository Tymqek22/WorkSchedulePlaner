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
				.HasOne<ApplicationUser>()
				.WithMany()
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
