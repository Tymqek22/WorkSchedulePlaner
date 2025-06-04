using Microsoft.AspNetCore.Identity;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Infrastructure.Identity.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public ICollection<WorkSchedule> SchedulesOwned { get; set; }
		public ICollection<ScheduleUser> SchedulesAssigned { get; set; }
		public ICollection<Employee> AssignedEmployees { get; set; }
	}
}
