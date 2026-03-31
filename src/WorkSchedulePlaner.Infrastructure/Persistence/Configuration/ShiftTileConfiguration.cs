using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Infrastructure.Persistence.Configuration
{
	public class ShiftTileConfiguration : IEntityTypeConfiguration<ShiftTile>
	{
		public void Configure(EntityTypeBuilder<ShiftTile> builder)
		{
			builder.HasKey(st => st.Id);

			builder.OwnsMany(x => x.Assignments,a =>
			{
				a.ToTable("ShiftTilesAssignments");
				a.WithOwner().HasForeignKey("ShiftTileId");
				a.Property<int>("Id");
				a.HasKey("Id");

				a.OwnsOne(x => x.TimeRange,t =>
				{
					t.Property(x => x.Start).HasColumnName("StartTime");
					t.Property(x => x.End).HasColumnName("EndTime");
				});
			});
		}
	}
}
