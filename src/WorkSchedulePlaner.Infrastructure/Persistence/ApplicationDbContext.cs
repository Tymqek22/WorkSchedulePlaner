using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Domain.Entities;


namespace WorkSchedulePlaner.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) {}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<ShiftTile> ShiftTiles { get; set; }
		public DbSet<WorkSchedule> WorkSchedules { get; set; }
		public DbSet<EmployeeShift> EmployeesShifts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EmployeeShift>()
				.HasKey(es => new { es.EmployeeId,es.ShiftTileId });

			modelBuilder.Entity<EmployeeShift>()
				.HasOne(e => e.Employee)
				.WithMany(es => es.EmployeeShifts)
				.HasForeignKey(es => es.EmployeeId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<EmployeeShift>()
				.HasOne(s => s.ShiftTile)
				.WithMany(es => es.EmployeeShifts)
				.HasForeignKey(es => es.ShiftTileId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<EmployeeShift>().ToTable("EmployeesShifts");
		}
	}
}
