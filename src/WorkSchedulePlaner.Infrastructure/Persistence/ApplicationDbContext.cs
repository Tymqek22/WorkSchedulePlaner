using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Identity.Models;
using WorkSchedulePlaner.Infrastructure.Persistence.Configuration;


namespace WorkSchedulePlaner.Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<ShiftTile> ShiftTiles { get; set; }
		public DbSet<WorkSchedule> WorkSchedules { get; set; }
		public DbSet<EmployeeShift> EmployeesShifts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new EmployeeShiftConfiguration());
			modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
			modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
		}
	}
}
