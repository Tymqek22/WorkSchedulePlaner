using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Infrastructure.Persistence.Configuration
{
	public class EmployeeShiftConfiguration : IEntityTypeConfiguration<EmployeeShift>
	{
		public void Configure(EntityTypeBuilder<EmployeeShift> builder)
		{
			builder.HasKey(es => new { es.EmployeeId,es.ShiftTileId });

			builder
				.HasOne(e => e.Employee)
				.WithMany(es => es.EmployeeShifts)
				.HasForeignKey(es => es.EmployeeId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(s => s.ShiftTile)
				.WithMany(es => es.EmployeeShifts)
				.HasForeignKey(es => es.ShiftTileId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.ToTable("EmployeesShifts");
		}
	}
}
